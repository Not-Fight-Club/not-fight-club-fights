using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotFightClub_Models.ViewModels
{
    public class ViewCharacter
    {
        //public Character()
        //{
        //    FightLoserNavigations = new HashSet<Fight>();
        //    FightWinnerNavigations = new HashSet<Fight>();
        //    Fighters = new HashSet<Fighter>();
        //}

        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int? Level { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Ties { get; set; }
        public string Baseform { get; set; }
        public Guid UserId { get; set; }
        public int TraitId { get; set; }
        public int WeaponId { get; set; }

        //public virtual Trait Trait { get; set; }
        //public virtual UserInfo User { get; set; }
        //public virtual Weapon Weapon { get; set; }
        //public virtual ICollection<Fight> FightLoserNavigations { get; set; }
        //public virtual ICollection<Fight> FightWinnerNavigations { get; set; }
        //public virtual ICollection<Fighter> Fighters { get; set; }
    }
}

