using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightsApi.Controllers
{
  [Route("api")]
  [ApiController]
  public class FightController : Controller
  {

    private readonly IRepository<ViewFight, int> _fr;

    public FightController(IRepository<ViewFight, int> fr)
    {
      _fr = fr;
    }

    [HttpGet("/fight/{id}")]
    public async Task<ActionResult<ViewFight>> Get(int id)
    {
      ViewFight fight = await _fr.Read(id);
      return Ok(fight);
    }

    [HttpPost("/fight/private/{CreateID}/{startDate}/{endDate}")]
    public async Task<ActionResult<ViewFight>> CreatePrivateFight(string CreateID, DateTime startDate, DateTime endDate)
    {
      ViewFight fight = new ViewFight()
      {
        Weather = 1,//new Random().Next(1, 10),
        Location = new Random().Next(1, 10),
        StartDate = startDate,
        EndDate = endDate,
        CreatorId = Guid.Parse(CreateID),
        PublicFight = false
      };
      var fightCreate = await _fr.Add(fight);
      return Ok(fightCreate);
    }

    [HttpPost("/fight/public/{CreateID}/{startDate}/{endDate}")]
    public async Task<ActionResult<ViewFight>> CreatePublicFight(string CreateID, DateTime startDate, DateTime endDate)
    {
      ViewFight fight = new ViewFight()
      {
        Weather = 1,//new Random().Next(1, 10),
        Location = new Random().Next(1, 10),
        StartDate = startDate,
        EndDate = endDate,
        CreatorId = Guid.Parse(CreateID),
        PublicFight = true
      };
      var fightCreate = await _fr.Add(fight);
      return Ok(fightCreate);
    }


    [HttpGet("/fight/current")]
    public async Task<ActionResult<ViewFight>> GetCurrent()

    {
      List<ViewFight> fights = await _fr.Read();
      ViewFight fight = fights.Last();
      return Ok(fight);
    }

    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewFight>>> All()
    {
      List<ViewFight> fights = await _fr.Read();
      return Ok(fights);

    }
  }
}
