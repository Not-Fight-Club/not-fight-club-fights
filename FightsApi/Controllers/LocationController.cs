using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightsApi.Controllers
{
  public class LocationController : Controller
  {
    private readonly IRepository<ViewLocation, int> _lo;

    public LocationController(IRepository<ViewLocation, int> lo)
    {
      _lo = lo;
    }



    [HttpGet("/[Controller]/[action]")]
    public async Task<ActionResult<List<ViewLocation>>> All()
    {
      List<ViewLocation> location = await _lo.Read();
      return Ok(location);

    }

    [HttpPost("/location")]
    public async Task<ActionResult<ViewLocation>> AddLocation([FromBody] ViewLocation viewLocation)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add trait
      //return the result
      //Console.WriteLine(viewTrait);
      var newLocation = await _lo.Add(viewLocation);
      return Ok(newLocation);
    }


  }
}
