using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FightsApi_Buisiness.Repositories
{
	public class CharacterRepository : IRepository<ViewCharacter, int>
	{
    private IHttpClientFactory _clientFactory;
    private ILogger<CharacterRepository> _logger;
    private IConfiguration _config;
    public CharacterRepository(IHttpClientFactory clientFactory, ILogger<CharacterRepository> logger, IConfiguration config)
    {
      _clientFactory = clientFactory;
      _logger = logger;
      _config = config;
    }


    public Task<ViewCharacter> Add(ViewCharacter obj)
		{
			throw new NotImplementedException();
		}

		public async Task<ViewCharacter> Read(int characterId)
		{
      // Access microservice for characters
      string baseUrl = _config["CharactersApiURL"];
      //string endpointURI = $"{baseUrl}/Character/{characterId}";
      string endpointURI = $"{baseUrl}/Character/{characterId}";
      var request = new HttpRequestMessage(HttpMethod.Get, endpointURI);
      var client = _clientFactory.CreateClient();
      _logger.LogInformation($"base address for client api: {client.BaseAddress}");
      var response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        string responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ViewCharacter>(responseJson);
      }
      else
      {
        _logger.LogError($"Failed request to {endpointURI}: {response}");
        return null;
      }
		}

		public async Task<List<ViewCharacter>> Read()
		{
      //get all characters
      string baseUrl = _config["CharactersApiURL"];
      //string endpointURI = $"{baseUrl}/Character/{characterId}";
      string endpointURI = $"{baseUrl}/Character";
      var request = new HttpRequestMessage(HttpMethod.Get, endpointURI);
      var client = _clientFactory.CreateClient();
      _logger.LogInformation($"base address for client api: {client.BaseAddress}");
      var response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        string responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ViewCharacter>>(responseJson);
      }
      else
      {
        _logger.LogError($"Failed request to {endpointURI}: {response}");
        return null;
      }

    
		}

		public Task<ViewCharacter> Update(ViewCharacter obj)
		{
			throw new NotImplementedException();
		}
	}
}
