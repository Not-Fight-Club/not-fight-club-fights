using FightsApi_Buisiness.Interfaces;
using FightsApi_Buisiness.Repositiories;
using FightsApi_Data;
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

    private readonly IFightRepository _fr;

    public FightController(IFightRepository fr)
    {
      _fr = fr;
    }

    [HttpGet("/fight/{id}")]
    public async Task<ActionResult<ViewFight>> Get(int id)
    {
      ViewFight fight = await _fr.Read(id);
      return Ok(fight);
    }


    [HttpGet("/fight/current")]
    public async Task<ActionResult<ViewFight>> GetCurrent()

    {
      List<ViewFight> fights = await _fr.Read();
      ViewFight fight = fights.Last();
      return Ok(fight);
    }

    //list all previous fights 
    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewFight>>> All()
    {
      List<ViewFight> fights = await _fr.Read();
      return Ok(fights);

    }

    //list all previous fights for a user
    [HttpGet("/fight/byuser/{id}")]
    public async Task<ActionResult<List<ViewFight>>> GetFightsByUserId(Guid id)
    {
      List<ViewFight> userFights = await _fr.FindFightsByUserId(id);
      return Ok(userFights);
    }
  }
}
