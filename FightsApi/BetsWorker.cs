using BetsApi_Models.ViewModels;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FightsApi
{
  public class BetsWorker : BackgroundService
  {

    private int DELAY_BETWEEN_CYCLES_MILLIS = 7000;

    private IHttpClientFactory _httpFactory;
    private IConfiguration _config;
    private ILogger<BetsWorker> _logger;
    private IServiceProvider _services;
    public BetsWorker(
      //P3_NotFightClubContext context,
      IHttpClientFactory httpFactory,
      IConfiguration config,
      ILogger<BetsWorker> logger,
      IServiceProvider services
      )
    {
      _httpFactory = httpFactory;
      _config = config;
      _logger = logger;
      _services = services;
    }

    private int CompareByEndDate(Fight f1, Fight f2)
    {
      if (f1.EndDate < f2.EndDate)
      {
        return -1;
      }
      else if (f1.EndDate > f2.EndDate)
      {
        return 1;
      }
      else
      {
        return 0;
      }
    }

    private async Task<List<ViewUser>> GetPayouts(Fight f, Fighter winner, CancellationToken cancelToken)
    {
      string baseUrl = _config["apiUrl:bets"];
      string endpointURI = $"{baseUrl}/api/wagers/{f.FightId}/{winner.FighterId}";
      _logger.LogInformation($"Getting payouts from url '{endpointURI}'");
      var request = new HttpRequestMessage(HttpMethod.Get, endpointURI);
      var client = _httpFactory.CreateClient();
      _logger.LogInformation($"base address for bets api: {client.BaseAddress}");
      var response = await client.SendAsync(request, cancelToken);
      if (response.IsSuccessStatusCode)
      {
        string responseJson = await response.Content.ReadAsStringAsync(cancelToken);
        return JsonConvert.DeserializeObject<List<ViewUser>>(responseJson);
      }
      else
      {
        throw new HttpRequestException("Error fetching payouts");
      }

    }

    private async Task<bool> SendPayouts(List<ViewUser> payouts, CancellationToken cancelToken)
    {
      string baseUrl = _config["apiUrl:users"];
      string endpointURI = $"{baseUrl}/UpdateTotalList";
      _logger.LogInformation($"Sending payouts to url '{endpointURI}'");
      var request = new HttpRequestMessage(HttpMethod.Post, endpointURI);
      var jsonBody = JsonConvert.SerializeObject(payouts);
      request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
      var client = _httpFactory.CreateClient();
      var response = await client.SendAsync(request, cancelToken);
      return response.IsSuccessStatusCode;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Starting bets payout background task");
      using (var scope = _services.CreateScope())
      {
        using (var context = scope.ServiceProvider.GetRequiredService<P3_NotFightClubContext>())
        {
          while (!stoppingToken.IsCancellationRequested)
          {
            _logger.LogInformation("Running payouts checker");
            try
            {
              await DoPayouts(context, stoppingToken);
            }
            catch (Exception e)
            {
              _logger.LogError($"Error encountered processing fight payouts: {e}");
            }
            _logger.LogInformation($"Finished payouts checker. Waiting {DELAY_BETWEEN_CYCLES_MILLIS} milliseconds");
            await Task.Delay(DELAY_BETWEEN_CYCLES_MILLIS, stoppingToken);
          }
        }
      }
    }
    private async Task DoPayouts(P3_NotFightClubContext context, CancellationToken stoppingToken)
    {
      // get all fights
      var fights = await (
        from f in context.Fights
        where f.Finished == false
        orderby f.EndDate
        select f
        ).ToListAsync(stoppingToken);
      //fights.Sort(CompareByEndDate);

      if (fights.Count < 1)
      {
        _logger.LogInformation("No fights found for payouts service processing");
        // no fights found, no processing to do
        return;
      }
      _logger.LogInformation($"Checking {fights.Count} fights for payout info");
      foreach(var f in fights)
      {
        var nextFightTime = f.EndDate;
        if (nextFightTime > DateTime.Now)
        {
          // no fights have ended that haven't been processed, wait for more
          // NOTE: with a better implementation, we could have this process wait for the EndDate,
          //      but the way it is now, we want to keep looking for fights that could be added
          //      before the next fight ends
          _logger.LogInformation($"Next fight ends at {nextFightTime}");
          break;
        }
        _logger.LogInformation($"Processing fight by id {f.FightId}");
        // update data in fight and payout bets
        var fighters = await (from fi in context.Fighters where fi.FightId == f.FightId select fi).ToListAsync(stoppingToken);
        var winningFighter = await context.Fighters.FromSqlRaw(
          "SELECT * FROM Fighter WHERE FighterId = ( " +
            "SELECT TOP(1) f.FighterId " +
            "FROM Fighter f " +
            "INNER JOIN Votes v " +
            "ON f.FighterId = v.FighterId AND v.FightId = {0} " +
            "GROUP BY f.FighterId " +
            "ORDER BY Count(v.VoteId) DESC " +
          ")",
          f.FightId
          )
          .AsTracking()
          .FirstOrDefaultAsync(stoppingToken);
        if (winningFighter == null)
        {
          _logger.LogInformation($"No winning fighter found for fight {f.FightId}");
          continue;
        }
        _logger.LogInformation($"Winning fighter {winningFighter.FighterId} found for fight {f.FightId}");
        winningFighter.IsWinner = true;
        var payoutsForUsers = await GetPayouts(f, winningFighter, stoppingToken);
        _logger.LogInformation($"Got info on {payoutsForUsers.Count} bets that will pay out");
        _logger.LogInformation("Sending payments to users service...");
        if (await SendPayouts(payoutsForUsers, stoppingToken))
        {
          _logger.LogInformation("Payments successful");
          f.Finished = true;
          await context.SaveChangesAsync();
        }
      }
    }
  }
}
