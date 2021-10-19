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

    [HttpPost("/weather")]
    public async Task<ActionResult<ViewWeather>> AddWeather([FromBody] ViewWeather viewWeather)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add trait
      //return the result
      //Console.WriteLine(viewTrait);
      var newWeather = await _we.Add(viewWeather);
      return Ok(newWeather);
    }
  }
