using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using FightsApi_Data;

namespace FightsApi_Buisiness.Repositiories
{
  public class FightRepository : IFightRepository
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Fight, ViewFight> _mapper;
    private readonly IRepository<ViewCharacter, int> _characterRepo;

    public FightRepository(IMapper<Fight, ViewFight> mapper, P3_NotFightClubContext dbContext,IRepository<ViewCharacter,int> characterRepo)
    {
      _mapper = mapper;
      _dbContext = dbContext;
      _characterRepo = characterRepo;


    }

    public Task<ViewFight> Add(ViewFight obj)
    {
      throw new NotImplementedException();
    }


		public async Task<ViewFight> Read(int obj)
    {
      Fight fight = await Task.Run(() => _dbContext.Fights.Find(obj));

      return _mapper.ModelToViewModel(fight);
    }

    public async Task<List<ViewFight>> Read()
    {

      List<Fight> fights = await _dbContext.Fights
        .Include("WeatherNavigation")
        .Include("LocationNavigation")
        .Include(fight => fight.Fighters)
        .ThenInclude(fighter => fighter.Votes)
        .ToListAsync();

      return _mapper.ModelToViewModel(fights);
    }

    public Task<ViewFight> Update(ViewFight obj)
    {
      throw new NotImplementedException();
    }

    //find all fights by userId
    public async Task<List<ViewFight>> FindFightsByUserId(Guid userId)
    {//
     List<ViewCharacter> allCharacters= await _characterRepo.Read();

      List<int> cIdForUser = allCharacters.Where(cs => cs.UserId == userId).Select(C => C.CharacterId).ToList();
      List<int> fightIds = await _dbContext.Fighters.Where(f => cIdForUser.Contains(f.CharacterId)).Select(f => f.FightId).ToListAsync();

      
    List<ViewFight> userFights = await _dbContext.Fights.Where(f =>fightIds.Contains(f.FightId)).Include("WeatherNavigation")
        .Include("LocationNavigation").Include(f=>f.Fighters)
        .ThenInclude(fighter => fighter.Votes)
        .Select(f=> _mapper.ModelToViewModel(f)).ToListAsync();
      return userFights;
      //var fights = await _dbContext.Fights.Where(f => f.FightId == userId).Join(_dbContext.Fighters, fid1 => fid)
    }

		public Task<List<ViewFight>> FindFightsByUserId(int userId)
		{
			throw new NotImplementedException();
		}
	}
}
