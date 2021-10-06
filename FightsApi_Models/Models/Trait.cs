using System;
using System.Collections.Generic;

#nullable disable

namespace FightsApi_Models.Models
{
    public partial class Trait
    {
        public Trait()
        {
            Characters = new HashSet<Character>();
        }

        public int TraitId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
