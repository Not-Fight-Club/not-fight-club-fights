using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightsApi.Controllers
{
  public class WeatherController : Controller
  {
    private readonly IRepository<ViewWeather, int> _we;

    public WeatherController(IRepository<ViewWeather, int> we)
    {
      _we = we;
    }



    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewWeather>>> All()
    {
      List<ViewWeather> weather = await _we.Read();
      return Ok(weather);

    }

    [HttpPost("/weather")]
    public async Task<ActionResult<ViewWeather>> AddLocation([FromBody] ViewWeather viewWeather)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add trait
      //return the result
      //Console.WriteLine(viewTrait);
      var newWeather = await _we.Add(viewWeather);
      return Ok(newWeather);
    }
  }
}

