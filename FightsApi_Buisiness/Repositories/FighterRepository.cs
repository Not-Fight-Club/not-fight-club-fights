using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FightsApi_Buisiness.Repositories
{
  public class FighterRepository : IRepository<ViewFighter, int>
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Fighter, ViewFighter> _mapper;

    public FighterRepository(IMapper<Fighter, ViewFighter> mapper, P3_NotFightClubContext dbContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task<ViewFighter> Add(ViewFighter obj)
    {
      Fighter newFighter = new Fighter()
      {
        CharacterId = obj.CharacterId,
        FightId = obj.FightId
      };

      _dbContext.Fighters.Add(newFighter);
      await _dbContext.SaveChangesAsync();
      return _mapper.ModelToViewModel(newFighter);
    }

    public Task<ViewFighter> Read(int obj)
    {
      throw new NotImplementedException();
    }

    public async Task<List<ViewFighter>> Read()
    {
      List<Fighter> fighters = await _dbContext.Fighters.ToListAsync();
      return _mapper.ModelToViewModel(fighters);
    }

    public Task<ViewFighter> Update(ViewFighter obj)
    {
      throw new NotImplementedException();
    }
  }
}