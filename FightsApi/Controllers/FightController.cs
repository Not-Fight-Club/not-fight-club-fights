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

    private readonly IFightRepository _fr;
    private readonly IRepository<ViewFighter, int> _fighterrepo;
    private readonly IWeatherRepository _wr;

    public FightController(IFightRepository fr, IRepository<ViewFighter, int> fighterrepo, IWeatherRepository wr)
    {
      _fr = fr;
      _fighterrepo = fighterrepo;
      _wr = wr;

    }


    private async Task<List<ViewFighter>> AddFightersToFight(List<ViewCharacter> viewFighters, int fightId)
    {
      List<ViewFighter> result = new List<ViewFighter>();
      foreach (var f in viewFighters)
      {
        ViewFighter viewf = new ViewFighter();
        viewf.FightId = fightId;
        viewf.CharacterId = f.CharacterId;
        var saved = await _fighterrepo.Add(viewf);
        result.Add(saved);

      }
      return result;
    }

    [HttpPost("/fight/private")]
    public async Task<ActionResult<ViewFight>> CreatePrivateFight(ViewFightCharacter viewFight)
    {
      if(viewFight.Weather == 0)
			{
        viewFight.Weather = (await _wr.ReadRandom()).WeatherId;
			}
      ViewFight fight = new ViewFight()
      {
        Weather = viewFight.Weather,
        Location = viewFight.Location,
        StartDate = viewFight.StartDate,
        EndDate = viewFight.EndDate,
        CreatorId = viewFight.CreatorId,
        PublicFight = false
      };
      var fightCreate = await _fr.Add(fight);
      if (fight.StartDate < fight.EndDate)
      {
       
        await AddFightersToFight(viewFight.Characters, fightCreate.FightId);
       
      }
      else
      {
        Console.WriteLine("Start Date must be < than EndDate");
      }
      return Ok(fightCreate);
    }

    [HttpPost("/fight/public")]
    public async Task<ActionResult<ViewFight>> CreatePublicFight(ViewFightCharacter viewFight)
    {
      if (viewFight.Weather == 0)
      {
        viewFight.Weather = (await _wr.ReadRandom()).WeatherId;
      }
      ViewFight fight = new ViewFight()
      {
        Weather = viewFight.Weather,
        Location = viewFight.Location,
        StartDate = viewFight.StartDate,
        EndDate = viewFight.EndDate,
        CreatorId = viewFight.CreatorId,
        PublicFight = true

      };
      var fightCreate = await _fr.Add(fight);
      if (fight.StartDate < fight.EndDate)
      {

        await AddFightersToFight(viewFight.Characters, fightCreate.FightId);

      }
      else
      {
        Console.WriteLine("Start Date must be < than EndDate");
      }
      return Ok(fightCreate);
    }

    [HttpGet("/fight/allbyCreatorId/{creatorID}")]
    public async Task<ActionResult<List<ViewFight>>> GetAllbyCreatorId(Guid creatorID)

    {
      List<ViewFight> fights = await _fr.ReadByCreatorID(creatorID, true);
      // ViewFight fight = fights.Last();
      return Ok(fights);
    }

    [HttpGet("/fight/allbyFightType/{fighType}")]
    public async Task<ActionResult<List<ViewFight>>> GetAllbyFightType(bool fighType)

    {
      List<ViewFight> fights = await _fr.ReadByFightType(fighType);
      // ViewFight fight = fights.Last();
      return Ok(fights);
    }

    [HttpGet("/fight/allbyCharacterID/{characterID}")]
    public async Task<ActionResult<List<ViewFight>>> GetAllbyCharacterID(int characterID)

    {
      List<ViewFight> fights = await _fr.ReadByCharacterID(characterID);
      // ViewFight fight = fights.Last();
      return Ok(fights);
    }

    [HttpGet("/fight/allbyCharacterCreatorID/{characterID}")]
    public async Task<ActionResult<List<ViewFight>>> GetAllbyCharacterCreatorID(int characterID)

    {
      List<ViewFight> fights = await _fr.ReadByCharacterID(characterID);
      // ViewFight fight = fights.Last();
      return Ok(fights);
    }


    [HttpGet("/fight/current")]
    public async Task<ActionResult<ViewFight>> GetCurrent()

    {
      List<ViewFight> fights = await _fr.Read();
      ViewFight fight = fights.Last();
      return Ok(fight);
    }

    [HttpGet("/fight/ongoing")]
    public async Task<ActionResult<ViewFight[]>> GetAllOngoing()

    {
      ViewFight[] fights = await _fr.Ongoing();
      return Ok(fights);
    }

    //list all previous fights 
    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewFight>>> All()
    {
      List<ViewFight> fights = await _fr.Read();
      return Ok(fights);

    }
    [HttpGet("/fight/{id}")]
    public async Task<ActionResult<ViewFight>> Get(int id)
    {
      ViewFight fight = await _fr.Read(id);
      return Ok(fight);
    }
  }
}
