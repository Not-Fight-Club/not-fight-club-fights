using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotFightClub_Models.ViewModels
{
    public class ViewTrait
    {
        public ViewTrait(int traitId, string description)
        {
            TraitId = traitId;
            Description = description;
        }

        public ViewTrait() { }

        //public Trait()
        //{
        //    Characters = new HashSet<Character>();
        //}

        public int TraitId { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<Character> Characters { get; set; }
    }
}
