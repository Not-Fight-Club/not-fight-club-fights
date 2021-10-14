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
    public async void VotesProperlyCast()
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

        ViewVote expected = new ViewVote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = 1
        };

        Fight testFight = new Fight()
        {
          FightId = 1,
          StartDate = null,
          EndDate = null,
          Location = 1,
          Weather = 1,
          Public = true,
          CreatorId = null
        };

        await mockDbContext.Fights.AddAsync(testFight);
        await mockDbContext.SaveChangesAsync();

        await voterepotest.Add(expected);
        await mockDbContext.SaveChangesAsync();

        Vote actual = await mockDbContext.Votes.FindAsync(1);

        Assert.NotNull(await mockDbContext.Fights.FindAsync(expected.FightId));

        Assert.Equal(expected.VoteId, actual.VoteId);
        Assert.Equal(expected.FightId, actual.FightId);
        Assert.Equal(expected.FighterId, actual.FighterId);
        Assert.Equal(expected.UserId, actual.UserId);

        //Attempt to test vote uniqueness
        //await Assert.ThrowsAnyAsync<DbUpdateException>(() => voterepotest.Add(vote));
      }
    }

    [Fact]
    public async void NullFightFails()
    {
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        ViewVote testVote = new ViewVote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = 1
        };

        Assert.Null(await mockDbContext.Fights.FindAsync(1));
        Assert.ThrowsAsync<NullReferenceException>(async () => await voterepotest.Add(testVote));
      }
    }

    [Fact]
    public async void ReadVote_Test()
    {
      using(var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        Vote expected = new Vote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = 1
        };

        await mockDbContext.Votes.AddAsync(expected);
        await mockDbContext.SaveChangesAsync();

        ViewVote actual = await voterepotest.Read(1);

        Assert.Equal(expected.VoteId, actual.VoteId);
        Assert.Equal(expected.FightId, actual.FightId);
        Assert.Equal(expected.FighterId, actual.FighterId);
        Assert.Equal(expected.UserId, actual.UserId);
      }
    }
  }
}
