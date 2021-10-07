using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_DbContext
{
    public partial class Fighter
    {
        public Fighter()
        {
            Votes = new HashSet<Vote>();
        }

        public int FighterId { get; set; }
        public int FightId { get; set; }
        public int CharacterId { get; set; }
        public bool IsWinner { get; set; }

        public virtual Fight Fight { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
