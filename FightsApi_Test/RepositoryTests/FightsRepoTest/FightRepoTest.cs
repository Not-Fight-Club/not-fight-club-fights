using FightsApi_Buisiness.Repositiories;
using FightsApi_Data;
using FightsApi_Logic.Mappers;
using FightsApi_Models.ViewModels;
using FightsApi_Test.RepositoryTests.DatabaseMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FightsApi_Test.RepositoryTests.FightsRepoTest
{
  [Collection("Repository Tests")]
  public class FightRepoTest
  {
    static Guid? z = new Guid();
    public static IEnumerable<object[]> GetData()
    {
      Fight test = new Fight();

      int? j = 1;
      int? n = 1;


      for (var i = 1; i <= 1000; i++)
      {
        yield return new object[] { i, z, DateTime.Now, DateTime.Now, j, n };
      }
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task Test_FightRepo(int id, Guid creatorId, DateTime startTime, DateTime endTime, int locationId, int weatherId)
    {

      // set up an in-memory version of NotFightClub
      using (var mockDbContext = FightsApiMock.GetDbContext())
      {
        // Delete the database if it already exists
        mockDbContext.Database.EnsureDeleted();
        // Re-make the database
        mockDbContext.Database.EnsureCreated();

        var fight = new Fight()
        {
          FightId = id,
          CreatorId = creatorId,
          StartDate = startTime,
          EndDate = endTime,
          Location = locationId,
          Weather = weatherId
        };
        mockDbContext.Fights.Add(fight);
        mockDbContext.SaveChanges();

        // TODO: set up a null logger and a mocked htttpClient
        var fightmapper = new FightMapper();
        var fightrepotest = new FightRepository(fightmapper, mockDbContext);
        List<ViewFight> fighties = new List<ViewFight>();
        fighties = await fightrepotest.Read();

        Assert.NotEmpty(fighties);
        foreach (var element in fighties)
        {
          Assert.Equal(id, fight.FightId);
          Assert.Equal(creatorId, fight.CreatorId);
          Assert.Equal(startTime, fight.StartDate);
          Assert.Equal(endTime, fight.EndDate);
          Assert.Equal(locationId, fight.Location);
          Assert.Equal(weatherId, fight.Weather);

        }

      }
    }

  }
}
