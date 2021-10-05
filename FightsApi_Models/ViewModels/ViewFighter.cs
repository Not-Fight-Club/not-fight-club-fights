using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotFightClub_Models.ViewModels
{
    public class ViewFighter
    {
        //public Fighter()
        //{
        //    Wagers = new HashSet<Wager>();
        //}

        public int FighterId { get; set; }
        public int? FightId { get; set; }
        public int? CharacterId { get; set; }
        public int? Votes { get; set; }

        //public virtual Character Character { get; set; }
        //public virtual Fight Fight { get; set; }
        //public virtual ICollection<Wager> Wagers { get; set; }
    }
}
