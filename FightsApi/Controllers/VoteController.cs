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
    private readonly IRepository<ViewVote, int> _vr;

    public VoteController(IRepository<ViewVote, int> vr)
    {
      _vr = vr;
    }

    [HttpPost("/vote")]
    public async Task<ActionResult<ViewVote>> Vote(ViewVote vote)
    {
      var newVote = await _vr.Add(vote);
      return Ok(newVote);
    }
  }
}
