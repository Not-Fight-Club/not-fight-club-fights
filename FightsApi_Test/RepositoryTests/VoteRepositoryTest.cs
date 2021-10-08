using FightsApi_Buisiness.Mappers;
using FightsApi_Buisiness.Repositiories;
using FightsApi_Buisiness.Repositories;
using FightsApi_Data;
using FightsApi_Logic.Mappers;
using FightsApi_Models.ViewModels;
using FightsApi_Test.RepositoryTests.DatabaseMock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FightsApi_Test.RepositoryTests
{
  public class VoteRepositoryTest
  {
    [Fact]
    public async void VotesNotNull()
    {
      // set up an in-memory version of NotFightClub
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        ViewVote vote = new ViewVote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = 1
        };

        await voterepotest.Add(vote);
        await mockDbContext.SaveChangesAsync();

        Vote expected = await mockDbContext.Votes.FindAsync(1);

        Assert.Equal(expected.VoteId, vote.VoteId);
        Assert.Equal(expected.FightId, vote.FightId);
        Assert.Equal(expected.FighterId, vote.FighterId);
        Assert.Equal(expected.UserId, vote.UserId);

        //Attempt to test vote uniqueness
        //await Assert.ThrowsAnyAsync<DbUpdateException>(() => voterepotest.Add(vote));
      }
    }
  }
}
