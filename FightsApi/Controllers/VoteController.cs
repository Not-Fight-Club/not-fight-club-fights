using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightsApi_Models.ViewModels;
using FightsApi_Buisiness.Interfaces;

namespace FightsApi.Controllers
{
  [Route("api")]
  [ApiController]
  public class VoteController : Controller
  {
    private readonly IVoteRepository _vr;

    public VoteController(IVoteRepository vr)
    {
      _vr = vr;
    }

    [HttpPost("/vote")]
    public async Task<ActionResult<ViewVote>> Vote(ViewVote vote)
    {
      var newVote = await _vr.Add(vote);
      return Ok(newVote);
    }

    [HttpGet("/votes/{fightId}/{fighterId}")]
    public async Task<ActionResult<ViewVote[]>> VotesByChoice(int fightId, int fighterId)
    {
      ViewVote[] votes = await _vr.ReadbyChoice(fightId, fighterId);
      return Ok(votes);
    }
  }
}
