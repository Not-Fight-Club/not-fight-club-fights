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
  public class LocationMapper : IMapper<Location, ViewLocation>
  {
    public ViewLocation ModelToViewModel(Location obj)
    {
      
      ViewLocation viewLocation = new ViewLocation();
      if (viewLocation == null) { return null; }
      viewLocation.Location1 = obj.Location1;
      return viewLocation;
    }

    public List<ViewLocation> ModelToViewModel(List<Location> obj)
    {
      return obj.ConvertAll(ModelToViewModel);
    }

    public Location ViewModelToModel(ViewLocation obj)
    {
      throw new NotImplementedException();
    }

    public List<Location> ViewModelToModel(List<ViewLocation> obj)
    {
      throw new NotImplementedException();
    }
  }
}
