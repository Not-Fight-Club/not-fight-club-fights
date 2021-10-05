using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotFightClub_Models.ViewModels
{
    public class ViewUserInfo
    {
        //public UserInfo()
        //{
        //    Characters = new HashSet<Character>();
        //    Comments = new HashSet<Comment>();
        //    Wagers = new HashSet<Wager>();
        //}

        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Pword { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public int? Bucks { get; set; }

        //public virtual ICollection<Character> Characters { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ICollection<Wager> Wagers { get; set; }
    }
}
