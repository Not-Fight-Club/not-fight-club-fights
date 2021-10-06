using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Models.Models
{
    public partial class Wager
    {
        public int WagerId { get; set; }
        public Guid? UserId { get; set; }
        public int? FightId { get; set; }
        public int? Amount { get; set; }
        public int? FighterId { get; set; }

        public virtual Fight Fight { get; set; }
        public virtual Fighter Fighter { get; set; }
        public virtual UserInfo User { get; set; }
    }
}
