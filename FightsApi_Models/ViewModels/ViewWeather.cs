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

        public int WeatherId { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<Fight> Fights { get; set; }
    }
}