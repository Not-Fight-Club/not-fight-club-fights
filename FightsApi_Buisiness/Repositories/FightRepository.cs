using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotFightClub_Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using FightsApi_Data;

namespace NotFightClub_Logic.Repositiories
{
  public class FightRepository : IRepository<ViewFight, int>
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
  }
}
