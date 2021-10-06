using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightsApi.Controllers
{
  public class TestCharacterLinkController : Controller
  {

    private IRepository<ViewCharacter, int> _repo;
    public TestCharacterLinkController(IRepository<ViewCharacter, int> repo)
    {
      _repo = repo;
    }

    [HttpGet("{characterId}")]
    public async Task<ViewCharacter> GetCharacterById(int characterId)
    {
      return await _repo.Read(characterId);
    }
  }
}
