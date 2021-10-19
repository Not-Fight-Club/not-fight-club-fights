using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightsApi_Models.ViewModels;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.Exceptions;

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
      try
      {
        var newVote = await _vr.Add(vote);
        return Ok(newVote);
      }
      catch (VotingPeriodException e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("/votes/{fightId}/{fighterId}")]
    public async Task<ActionResult<int>> Tally(int fightId, int fighterId)
    {
      ViewVote[] votes = await _vr.ReadbyChoice(fightId, fighterId);
      int tally = votes.Length;
      return Ok(tally);
    }

    [HttpGet("/uservote/{fightId}/{userId}")]
    public async Task<ActionResult<bool>> CheckUserVote(int fightId, Guid userId)
    {
      return await _vr.CheckUserVote(fightId, userId);
    }
  }
}
