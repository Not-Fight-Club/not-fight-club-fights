using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Data
{
    public partial class Location
    {
        public Location()
        {
            Fights = new HashSet<Fight>();
        }

        public int LocationId { get; set; }
        public string Location1 { get; set; }

        public virtual ICollection<Fight> Fights { get; set; }
    }
}
