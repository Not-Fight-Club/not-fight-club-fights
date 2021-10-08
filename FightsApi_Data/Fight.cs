using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Data
{
    public partial class Fight
    {
        public Fight()
        {
            Fighters = new HashSet<Fighter>();
            Votes = new HashSet<Vote>();
        }

        public int FightId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Location { get; set; }
        public int? Weather { get; set; }
       // public Guid? CreatorId { get; set; }
        public bool? Public { get; set; }

        public virtual Location LocationNavigation { get; set; }
        public virtual Weather WeatherNavigation { get; set; }
        public virtual ICollection<Fighter> Fighters { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
