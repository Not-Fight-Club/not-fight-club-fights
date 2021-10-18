using FightsApi_Buisiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Models.ViewModels;
using FightsApi_Data;
using Microsoft.EntityFrameworkCore;

namespace FightsApi_Buisiness.Repositories
{
  public class WeatherRepository : IRepository<ViewWeather, int>
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Weather, ViewWeather> _mapper;

    public WeatherRepository(IMapper<Weather, ViewWeather> mapper, P3_NotFightClubContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }

    public async Task<ViewWeather> Add(ViewWeather viewWeather)
    {
      Weather weather = _mapper.ViewModelToModel(viewWeather);
      _dbContext.Database.ExecuteSqlInterpolated($"Insert into Weather(description) values({weather.Description})");
      _dbContext.SaveChanges();
      Weather newWeather = await _dbContext.Weathers.FromSqlInterpolated($"select * from Weather where Description = {weather.Description}").FirstOrDefaultAsync();
      return _mapper.ModelToViewModel(newWeather);
    }

    public Task<ViewWeather> Read(int obj)
    {
      throw new NotImplementedException();
    }

    public async Task<List<ViewWeather>> Read()
    {
      List<Weather> weather = await _dbContext.Weathers.ToListAsync();

      return _mapper.ModelToViewModel(weather);
    }

    public Task<ViewWeather> Update(ViewWeather obj)
    {
      throw new NotImplementedException();
    }
  }
}
