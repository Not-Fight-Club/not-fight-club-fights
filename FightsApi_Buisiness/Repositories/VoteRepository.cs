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
  public class VoteRepository : IVoteRepository
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

    public async Task<List<ViewVote>> Read()
    {
      List<Vote> votes = await _dbContext.Votes.ToListAsync();

      return _mapper.ModelToViewModel(votes);
    }

    public async Task<ViewVote[]> ReadbyChoice(int fightId, int fighterId)
    {
      List<ViewVote> votes = await Read();
      List<ViewVote> filteredVotes = new List<ViewVote>();

      foreach(ViewVote v in votes)
      {
        if(v.FightId == fightId && v.FighterId == fighterId)
        {
          filteredVotes.Add(v);
        }
      }
      return filteredVotes.ToArray();
    }

    public Task<ViewVote> Update(ViewVote obj)
    {
      throw new NotImplementedException();
    }
  }
}
