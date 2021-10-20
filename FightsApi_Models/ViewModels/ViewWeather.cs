using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.ViewModels
{
  public class ViewWeather
  {
    //public Weather()
    //{
    //    Fights = new HashSet<Fight>();
    //}

    public ViewWeather() { }
    public ViewWeather(int weatherId, string description)
    {
      WeatherId = weatherId;
      Description = description;
    }

    public int WeatherId { get; set; }
    public string Description { get; set; }

    //public virtual ICollection<Fight> Fights { get; set; }
  }
}
