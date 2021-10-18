using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.ViewModels
{
  public class ViewLocation
  {
    public ViewLocation()
    {
      //  Fights = new HashSet<Fight>();
    }

    public ViewLocation(int locationId, string location1)
    {
      LocationId = locationId;
      Location1 = location1;
    }


    public int LocationId { get; set; }
    public string Location1 { get; set; }

    //public virtual ICollection<Fight> Fights { get; set; }
  }
}
