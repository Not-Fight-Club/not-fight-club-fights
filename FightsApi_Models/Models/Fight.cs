using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Models.Models
{
    public partial class Fight
    {
        public Fight()
        {
            Comments = new HashSet<Comment>();
            Fighters = new HashSet<Fighter>();
            Wagers = new HashSet<Wager>();
        }

        public int FightId { get; set; }
        public int? Winner { get; set; }
        public int? Loser { get; set; }
        public DateTime? Date { get; set; }
        public string Result { get; set; }
        public int? Location { get; set; }
        public int? Weather { get; set; }

        public virtual Location LocationNavigation { get; set; }
        public virtual Character LoserNavigation { get; set; }
        public virtual Weather WeatherNavigation { get; set; }
        public virtual Character WinnerNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Fighter> Fighters { get; set; }
        public virtual ICollection<Wager> Wagers { get; set; }
    }
}
