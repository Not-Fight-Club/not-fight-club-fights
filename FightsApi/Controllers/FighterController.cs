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
  public class FighterController : Controller
  {
    private readonly IRepository<ViewFighter, int> _fr;

    public FighterController(IRepository<ViewFighter, int> fr)
    {
      _fr = fr;
    }

    [HttpGet("/current/fighters/{id}")]
    public async Task<ActionResult<ViewFighter[]>> GetCurrent(int id)
    {
      ViewFighter[] participants = new ViewFighter[2];
      List<ViewFighter> fighters = await _fr.Read();

      foreach(ViewFighter f in fighters)
      {
        if(f.FightId == id && participants[0] == null)
        {
          participants[0] = f;
        }
        else if(f.FightId == id && participants[1] == null)
        {
          participants[1] = f;
        }
      }

      return Ok(participants);
    }
  }
}
