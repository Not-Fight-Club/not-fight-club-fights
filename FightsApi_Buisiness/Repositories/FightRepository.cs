using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using FightsApi_DbContext;

namespace FightsApi_Buisiness.Repositiories
{
  public class FightRepository : IFightRepository
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Fight, ViewFight> _mapper;

    public FightRepository(IMapper<Fight, ViewFight> mapper, P3_NotFightClubContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
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
    public async Task<List<ViewFight>> FindFightsByUserId(int userId)
    {//
    List<ViewFight> userFights = await _dbContext.Fights.Where(f => f.FightId == userId).Include("WeatherNavigation")
        .Include("LocationNavigation").Include(fight => fight.Fighters).Include(f=>f.Fighters)
        .ThenInclude(fighter => fighter.Votes)
        .Select(f=> _mapper.ModelToViewModel(f)).ToListAsync();
      return userFights;
      //var fights = await _dbContext.Fights.Where(f => f.FightId == userId).Join(_dbContext.Fighters, fid1 => fid)
    }


  }
}
