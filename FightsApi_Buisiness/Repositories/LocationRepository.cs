using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Repositories
{
  public class LocationRepository : IRepository<ViewLocation, int>
  {
    private readonly P3_NotFightClubContext _dbContext;
    private readonly IMapper<Location, ViewLocation> _mapper;

    public LocationRepository(IMapper<Location, ViewLocation> mapper, P3_NotFightClubContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }
    public Task<Location> Add(Location obj)
    {
      throw new NotImplementedException();
    }

    public Task<ViewLocation> Add(ViewLocation obj)
    {
      throw new NotImplementedException();
    }

    public Task<Location> Read(ViewLocation obj)
    {
      throw new NotImplementedException();
    }

    public async Task<List<ViewLocation>> Read()
{
      List<Location> location = await _dbContext.Locations.ToListAsync();

      List<ViewLocation> viewLocations = new List<ViewLocation>();
        
      return viewLocations= _mapper.ModelToViewModel(location); 
    }

    public Task<ViewLocation> Read(int obj)
    {
      throw new NotImplementedException();
    }

    public Task<Location> Update(Location obj)
    {
      throw new NotImplementedException();
    }

    public Task<ViewLocation> Update(ViewLocation obj)
    {
      throw new NotImplementedException();
    }
  }
}
