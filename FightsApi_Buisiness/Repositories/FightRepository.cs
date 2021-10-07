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
  public class FightRepository : IRepository<ViewFight, int>
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Fight, ViewFight> _mapper;

    public FightRepository(IMapper<Fight, ViewFight> mapper, P3_NotFightClubContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }

    public async Task<ViewFight> Add(ViewFight obj)
    {
            var fight = new Fight()
            {
                FightId = obj.FightId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                Location = obj.Location,
                Weather = obj.Weather
            };
            _dbContext.Fights.Add(fight);
            await _dbContext.SaveChangesAsync();
            return _mapper.ModelToViewModel(fight);
        }

    public async Task<ViewFight> Read(int obj)
    {
      Fight fight = await Task.Run(() => _dbContext.Fights.Find(obj));

      return _mapper.ModelToViewModel(fight);
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
        .Include(fight => fight.Fighters)
        .ThenInclude(fighter => fighter.Votes)
        .ToListAsync();

      return _mapper.ModelToViewModel(fights);
    }

    public async Task<ViewFight> Update(ViewFight obj)
    {
            var loc = await(from u in _dbContext.Fights where obj.Location == u.Location select u).FirstAsync();
            var fight = await(from f in _dbContext.Fights where f.FightId == obj.FightId select f).Include(f => f.Location).FirstAsync();

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
                Location = obj.Location
            };
            _dbContext.Fights.Add(link);
            await _dbContext.SaveChangesAsync();
            return _mapper.ModelToViewModel(fight);
        }


    }
}
