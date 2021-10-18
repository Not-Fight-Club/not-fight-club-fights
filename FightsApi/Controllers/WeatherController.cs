using FightsApi_Buisiness.Interfaces;
using FightsApi_Buisiness.Repositories;
using FightsApi_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightsApi.Controllers
{
	[Route("api")]
	[ApiController]
  public class WeatherController : Controller
  {
		private IWeatherRepository _wr;
		public WeatherController(IWeatherRepository wr)
    {
			_wr = wr;
    }



    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewWeather>>> All()
    {
     
			List<ViewWeather> weathers = await _wr.Read();
			return Ok(weathers);
		}

    }
  }
