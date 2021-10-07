using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_DbContext
{
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int FightId { get; set; }
        public int FighterId { get; set; }
        public int UserId { get; set; }

        public virtual Fight Fight { get; set; }
        public virtual Fighter Fighter { get; set; }
    }
}
