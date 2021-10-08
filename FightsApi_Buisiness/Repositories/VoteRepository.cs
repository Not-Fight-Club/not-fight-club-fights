using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;

namespace FightsApi_Buisiness.Repositories
{
  public class VoteRepository : IRepository<ViewVote, int>
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Vote, ViewVote> _mapper;

    public VoteRepository(IMapper<Vote, ViewVote> mapper, P3_NotFightClubContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }

    public async Task<ViewVote> Add(ViewVote obj)
    {
      Vote vote = new Vote();
      vote.FightId = obj.FightId;
      vote.FighterId = obj.FighterId;
      vote.UserId = obj.UserId;

      _dbContext.Votes.Add(vote);
      await _dbContext.SaveChangesAsync();

      return _mapper.ModelToViewModel(vote);
    }

    public Task<ViewVote> Read(int obj)
    {
      throw new NotImplementedException();
    }

    public Task<List<ViewVote>> Read()
    {
      throw new NotImplementedException();
    }

    public Task<ViewVote> Update(ViewVote obj)
    {
      throw new NotImplementedException();
    }
  }
}
