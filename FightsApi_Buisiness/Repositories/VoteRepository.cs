using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.Exceptions;
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
      Fight fight = await _dbContext.Fights.FindAsync(obj.FightId);

      if(fight == null)
      {
        throw new FightNullException("The fight you tried to vote for is null");
      }
      else if(DateTime.Now < fight.StartDate)
      {
        throw new VotingPeriodException("Voting period has not begun");
      }
      else if(DateTime.Now > fight.EndDate)
      {
        throw new VotingPeriodException("Voting period has closed");
      }
      else
      {
        Vote vote = new Vote();
        vote.FightId = obj.FightId;
        vote.FighterId = obj.FighterId;
        vote.UserId = obj.UserId;

        _dbContext.Votes.Add(vote);
        await _dbContext.SaveChangesAsync();

        return _mapper.ModelToViewModel(vote);
      }

    }

    public async Task<ViewVote> Read(int obj)
    {
      Vote vote = await _dbContext.Votes.FindAsync(obj);

      return _mapper.ModelToViewModel(vote);
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

    public async Task<ViewVote> Update(ViewVote obj)
    {
      Vote vote = (from v in _dbContext.Votes
                   where v.VoteId == obj.VoteId
                   select v).FirstOrDefault();

      vote.FighterId = obj.FighterId;

      await _dbContext.SaveChangesAsync();

      return _mapper.ModelToViewModel(vote);
    }
  }
}
