using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Models.Models
{
    public partial class Weather
    {
        public Weather()
        {
            Fights = new HashSet<Fight>();
        }

        public int WeatherId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Fight> Fights { get; set; }
    }
}
