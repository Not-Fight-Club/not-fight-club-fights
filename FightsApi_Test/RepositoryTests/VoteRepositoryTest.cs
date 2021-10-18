using FightsApi_Buisiness.Mappers;
using FightsApi_Buisiness.Repositiories;
using FightsApi_Buisiness.Repositories;
using FightsApi_Data;
using FightsApi_Logic.Mappers;
using FightsApi_Models.Exceptions;
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
  [Collection("Repository Tests")]
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
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
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
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        Assert.Null(await mockDbContext.Fights.FindAsync(1));
        await Assert.ThrowsAsync<FightNullException>(async () => await voterepotest.Add(testVote));
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
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
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

    [Fact]
    public async void ReadVoteList_Test()
    {
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        Vote expected0 = new Vote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        Vote expected1 = new Vote()
        {
          VoteId = 2,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8E")
        };

        Vote expected2 = new Vote()
        {
          VoteId = 3,
          FightId = 1,
          FighterId = 2,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8F")
        };

        List<Vote> expected = new List<Vote>();
        expected.Add(expected0);
        expected.Add(expected1);
        expected.Add(expected2);

        await mockDbContext.Votes.AddRangeAsync(expected);
        await mockDbContext.SaveChangesAsync();

        List<ViewVote> actual = await voterepotest.Read();

        for(int i = 0; i<expected.Count; i++)
        {
          Assert.Equal(expected[i].VoteId, actual[i].VoteId);
          Assert.Equal(expected[i].FightId, actual[i].FightId);
          Assert.Equal(expected[i].FighterId, actual[i].FighterId);
          Assert.Equal(expected[i].UserId, actual[i].UserId);
        }
      }
    }

    [Fact]
    public async void VoteCanChange()
    {
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        Vote oldVote = new Vote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        Vote expected = new Vote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 2,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        ViewVote newVote = new ViewVote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 2,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        await mockDbContext.Votes.AddAsync(oldVote);
        await mockDbContext.SaveChangesAsync();

        await voterepotest.Update(newVote);

        Vote actual = await mockDbContext.Votes.FindAsync(1);

        Assert.Equal(expected.VoteId, actual.VoteId);
        Assert.Equal(expected.FightId, actual.FightId);
        Assert.Equal(expected.FighterId, actual.FighterId);
        Assert.Equal(expected.UserId, actual.UserId);
      }
    }

    [Fact]
    public async void ReadByChoice_Test()
    {
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var votemapper = new VoteMapper();
        var voterepotest = new VoteRepository(votemapper, mockDbContext);

        Vote expected0 = new Vote()
        {
          VoteId = 1,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8D")
        };

        Vote expected1 = new Vote()
        {
          VoteId = 2,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8E")
        };

        Vote expected2 = new Vote()
        {
          VoteId = 3,
          FightId = 1,
          FighterId = 2,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0055C2319A8F")
        };

        Vote expected3 = new Vote()
        {
          VoteId = 4,
          FightId = 1,
          FighterId = 2,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-1055C2319A8D")
        };

        Vote expected4 = new Vote()
        {
          VoteId = 5,
          FightId = 1,
          FighterId = 1,
          UserId = Guid.Parse("5CDF3AE3-C74D-46F7-A271-0155C2319A8D")
        };

        List<Vote> votes = new List<Vote>();
        votes.Add(expected0);
        votes.Add(expected1);
        votes.Add(expected2);
        votes.Add(expected3);
        votes.Add(expected4);


        await mockDbContext.Votes.AddRangeAsync(votes);
        await mockDbContext.SaveChangesAsync();

        ViewVote[] expectedArray1 = { votemapper.ModelToViewModel(expected0), votemapper.ModelToViewModel(expected1), votemapper.ModelToViewModel(expected4) };
        ViewVote[] expectedArray2 = { votemapper.ModelToViewModel(expected2), votemapper.ModelToViewModel(expected3) };

        ViewVote[] actualArray1 = await voterepotest.ReadbyChoice(1,1);
        ViewVote[] actualArray2 = await voterepotest.ReadbyChoice(1,2);


        for (int i = 0; i < expectedArray1.Length; i++)
        {
          Assert.Equal(expectedArray1[i].VoteId, actualArray1[i].VoteId);
          Assert.Equal(expectedArray1[i].FightId, actualArray1[i].FightId);
          Assert.Equal(expectedArray1[i].FighterId, actualArray1[i].FighterId);
          Assert.Equal(expectedArray1[i].UserId, actualArray1[i].UserId);
        }

        for (int i = 0; i < expectedArray2.Length; i++)
        {
          Assert.Equal(expectedArray2[i].VoteId, actualArray2[i].VoteId);
          Assert.Equal(expectedArray2[i].FightId, actualArray2[i].FightId);
          Assert.Equal(expectedArray2[i].FighterId, actualArray2[i].FighterId);
          Assert.Equal(expectedArray2[i].UserId, actualArray2[i].UserId);
        }
      }
    }
  }
}
