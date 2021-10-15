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
  }
}

