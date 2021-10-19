using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Interfaces
{
	public interface IWeatherRepository:IRepository<ViewWeather, string>
	{
		public Task <ViewWeather> ReadRandom();
	}
}
