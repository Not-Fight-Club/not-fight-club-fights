using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using FightsApi_Data;
using FightsApi_Buisiness.Mappers;
namespace FightsApi_Buisiness.Repositiories
{
  public class FightRepository : IFightRepository
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Fight, ViewFight> _mapper;
    private readonly IRepository<ViewCharacter, int> _characterRepo;
    private readonly CharacterFightMapper _fightCharacterMapper;

    public FightRepository(
      IMapper<Fight, ViewFight> mapper,
      P3_NotFightClubContext dbContext,
      IRepository<ViewCharacter, int> characterRepo,
     CharacterFightMapper fightCharacterMapper)
    {
      _mapper = mapper;
      _dbContext = dbContext;
      _characterRepo = characterRepo;
      _fightCharacterMapper = fightCharacterMapper;
    }

    public async Task<ViewFight> Add(ViewFight obj)
    {
      var fight = new Fight()
      {
        FightId = obj.FightId,
        StartDate = obj.StartDate,
        EndDate = obj.EndDate,
        Location = obj.Location,
        Weather = obj.Weather,
        Public = obj.PublicFight,
        CreatorId = obj.CreatorId
      };
      _dbContext.Fights.Add(fight);
      await _dbContext.SaveChangesAsync();
      return _mapper.ModelToViewModel(fight);
    }


    public async Task<ViewFight> Read(int obj)
    {
     Fight fight = await _dbContext.Fights.Include("WeatherNavigation")
      .Include("LocationNavigation").FirstOrDefaultAsync(f => f.FightId == obj);
      ViewFight viewFight = _mapper.ModelToViewModel(fight);
     return viewFight;
    }

    public async Task<List<ViewFight>> ReadByCreatorID(Guid obj, bool past)
    {
      List<Fight> fights = await Task.Run(() => _dbContext.Fights.Where(f => f.CreatorId == obj).Include("WeatherNavigation")
        .Include("LocationNavigation").ToList());
      List<ViewFight> viewFights = new List<ViewFight>();
      foreach (var fight in fights)
      {
        ViewFight viewFight = new ViewFight();
        if(past)
        {
          if (DateTime.Compare(DateTime.Now, DateTime.Parse(fight.EndDate.ToString())) < 0)
          {
            viewFight = _mapper.ModelToViewModel(fight);
            viewFights.Add(viewFight);
          }
        }
        else
        {
          viewFight = _mapper.ModelToViewModel(fight);
          viewFights.Add(viewFight);
        }
       
      }
      return viewFights;
    }
      public async Task<List<ViewFight>> ReadByFightType(bool obj)
      {
        List<Fight> fights = await Task.Run(() => _dbContext.Fights.Where(f => f.Public == obj).Include("WeatherNavigation")
        .Include("LocationNavigation").ToList());
        List<ViewFight> viewFights = new List<ViewFight>();
        foreach (var fight in fights)
        {
          ViewFight viewFight = new ViewFight();

        if (DateTime.Compare(DateTime.Now, DateTime.Parse(fight.EndDate.ToString())) < 0)
        {
          viewFight = _mapper.ModelToViewModel(fight);
          viewFights.Add(viewFight);
        }
      }

        return viewFights;
    }

    public async Task<ViewFight[]> Ongoing()
    {

      List<Fight> fights = await _dbContext.Fights.Include("WeatherNavigation").Include("LocationNavigation").ToListAsync();
      List<ViewFight> filteredFights = new List<ViewFight>();
      foreach (Fight fight in fights)
      {
        if ((DateTime.Compare(DateTime.Now, DateTime.Parse(fight.StartDate.ToString())) > 0) && (DateTime.Compare(DateTime.Now, DateTime.Parse(fight.EndDate.ToString())) < 0))
        {
          filteredFights.Add(_mapper.ModelToViewModel(fight));
        }
      }

      return filteredFights.ToArray();
    }

    public async Task<List<ViewFight>> ReadByCharacterID(int obj)
    {
      List<Fighter> fighters = await Task.Run(() => _dbContext.Fighters.Where(f => f.CharacterId == obj).ToList());
      List<ViewFight> viewFights = new List<ViewFight>();
      foreach (var fighter in fighters)
      {
        ViewFight viewFight = new ViewFight();
        Fight fight = await Task.Run(() => _dbContext.Fights.Where(f => f.FightId == fighter.FightId).Include("WeatherNavigation")
      .Include("LocationNavigation").ToList().Last());
        if (DateTime.Compare(DateTime.Now, DateTime.Parse(fight.EndDate.ToString())) < 0)
        {
          viewFight = _mapper.ModelToViewModel(fight);
          viewFights.Add(viewFight);
        }
      }

      return viewFights;
    }
    public async Task<List<ViewFight>> ReadByCharacterCreatorID(int obj)
    {
      List<ViewCharacter> allCharacters = await _characterRepo.Read();

      ViewCharacter cIdForUser = allCharacters.Where(C => C.CharacterId == obj).Last();

      List<Fight> fights = await Task.Run(() => _dbContext.Fights.Where(f => f.CreatorId == cIdForUser.UserId).ToList());
      List<ViewFight> viewFights = new List<ViewFight>();
      foreach (var fight in fights)
      {
        ViewFight viewFight = new ViewFight();
        if (DateTime.Compare(DateTime.Now, DateTime.Parse(fight.EndDate.ToString())) < 0)
        {
          viewFight = _mapper.ModelToViewModel(fight);
          viewFights.Add(viewFight);
        }
      }

      return viewFights;
    }

    public async Task<ViewFight> Read(DateTime startTime)
     {
       Fight fight = await Task.Run(() => _dbContext.Fights.Find(startTime));

      return _mapper.ModelToViewModel(fight);
    }

    public async Task<List<ViewFight>> Read()
    {

      List<Fight> fights = await _dbContext.Fights
        .Include("WeatherNavigation")
        .Include("LocationNavigation")

        .ToListAsync();

      return _mapper.ModelToViewModel(fights);
    }

    public async Task<ViewFight> Update(ViewFight obj)
    {
      var loc = await (from u in _dbContext.Fights where obj.Location == u.Location select u).FirstAsync();
      var fight = await (from f in _dbContext.Fights where f.FightId == obj.FightId select f).Include(f => f.Location).FirstAsync();

      var link = new Fight()
      {
        Location = obj.Location
      };
      _dbContext.Fights.Add(link);
      await _dbContext.SaveChangesAsync();
      return _mapper.ModelToViewModel(fight);
    }
    public async Task<ViewFight> UpdateWeather(ViewFight obj)
    {
      var weather = await (from u in _dbContext.Fights where obj.Weather == u.Weather select u).FirstAsync();
      var fight = await (from f in _dbContext.Fights where f.FightId == obj.FightId select f).Include(f => f.Weather).FirstAsync();

      var link = new Fight()
      {
        Weather = obj.Weather
      };
      _dbContext.Fights.Add(link);
      await _dbContext.SaveChangesAsync();
      return _mapper.ModelToViewModel(fight);
    }

    //find all fights by userId
    public async Task<List<ViewFightCharacter>> FindFightsByUserId(Guid userId)
    {//
      List<ViewCharacter> allCharacters = await _characterRepo.Read();

      List<int> cIdForUser = allCharacters.Where(cs => cs.UserId == userId).Select(C => C.CharacterId).ToList();
      List<int> fightIds = await _dbContext.Fighters.Where(f => cIdForUser.Contains(f.CharacterId)).Select(f => f.FightId).ToListAsync();

      List<ViewFightCharacter> userFights = await _dbContext.Fights.Where(f => fightIds.Contains(f.FightId)).Include("WeatherNavigation")
          .Include("LocationNavigation").Include(f => f.Fighters)
          .ThenInclude(fighter => fighter.Votes)
          .Select(f => _fightCharacterMapper.ModelToViewModel(f,allCharacters)).ToListAsync();

      return userFights;
      //var fights = await _dbContext.Fights.Where(f => f.FightId == userId).Join(_dbContext.Fighters, fid1 => fid)
    }

	}

}