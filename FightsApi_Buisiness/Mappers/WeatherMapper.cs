using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FightsApi_Buisiness.Mappers
{
 public class WeatherMapper : IMapper<Weather, ViewWeather>
  {
    public ViewWeather ModelToViewModel(Weather obj)
    {
      if (obj == null) { return null; }
        ViewWeather viewWeather = new ViewWeather();
        viewWeather.Description = obj.Description;
      return viewWeather;
    }

    public List<ViewWeather> ModelToViewModel(List<Weather> obj)
    {
      return obj.ConvertAll(ModelToViewModel); 
    }

    public Weather ViewModelToModel(ViewWeather obj)
    {
      throw new NotImplementedException();
    }

    public List<Weather> ViewModelToModel(List<ViewWeather> obj)
    {
      throw new NotImplementedException();
    }
  }
}
