using FightsApi_Buisiness.Repositiories;
using FightsApi_Data;
using FightsApi_Logic.Mappers;
using FightsApi_Models.ViewModels;
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

			public static IEnumerable<object[]> GetData()
			{
			Fight test = new Fight();
			int? j = test.Location;
			int? n = test.Weather;
			
				for (var i = 1; i <= 1000; i++)
				{
					yield return new object[] { i,  DateTime.Now, DateTime.Now, j, n};
				}
			}

			[Theory]
			[MemberData(nameof(GetData))]
			public async Task Test_RecipeRepo(int id, DateTime startTime, DateTime endTime, int locationId, int weatherId)
			{

				// set up an in-memory version of TheTofuWarriorsDBContext
				using (var mockDbContext = new P3_NotFightClubContext())
				{
					// Delete the database if it already exists
					mockDbContext.Database.EnsureDeleted();
					// Re-make the database
					mockDbContext.Database.EnsureCreated();

				var fight = new Fight()
				{
					FightId = id,
					//CreatorId = creatorId,
					StartDate = startTime,
				    EndDate = endTime,
				    Location = locationId,
					Weather = weatherId
					};
					mockDbContext.Fights.Add(fight);
					mockDbContext.SaveChanges();

				// TODO: set up a null logger and a mocked htttpClient
				var fightmapper = new FightMapper();
				var fightrepotest = new FightRepository(fightmapper, mockDbContext) ;
				List<ViewFight> fighties = new List<ViewFight>();
			     fighties= await fightrepotest.Read();

					Assert.NotEmpty(fighties);
					foreach (var element in fighties)
					{
						Assert.Equal(id, fight.FightId);
						//Assert.Equal(creatorId, fight.CreatorId);
						Assert.Equal(startTime, fight.StartDate);
						Assert.Equal(endTime, fight.EndDate);
						Assert.Equal(locationId, fight.Location);
					    Assert.Equal(weatherId, fight.Weather);
					    
				    }

				}
			}
		
	}
}
