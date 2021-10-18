using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Data
{
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int FightId { get; set; }
        public int FighterId { get; set; }
        public Guid UserId { get; set; }

        public virtual Fight Fight { get; set; }
        public virtual Fighter Fighter { get; set; }
    }
}
