using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FightsApi_Buisiness.Interfaces;

namespace FightsApi_Buisiness.Repositories
{
	public class WeatherRepository : IWeatherRepository
	{
		private readonly P3_NotFightClubContext _dbContext;
    //get all weather from db to pick a random weather
    public WeatherRepository(P3_NotFightClubContext dbContext)
		{
      _dbContext = dbContext;
    }

		public Task<ViewWeather> Add(ViewWeather obj)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ViewWeather>> Read()
    {
      List<ViewWeather> allWeather = await _dbContext.Weathers
        .Select(w => new ViewWeather
        {
          WeatherId = w.WeatherId,
          Description = w.Description
        }).ToListAsync();
      return allWeather;
    }
		public async Task <ViewWeather> ReadRandom()
		{
			List<ViewWeather> weathers = await this.Read();
			Random random = new Random();
			int id = random.Next(0, weathers.Count() - 1);
			return weathers[id];
		}

		public Task<ViewWeather> Read(int obj)
		{
			throw new NotImplementedException();
		}

		public Task<ViewWeather> Update(ViewWeather obj)
		{
			throw new NotImplementedException();
		}
	}
}
