using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NotFightClub_Models.Models;

namespace FightsApi_Models.ViewModels
{
  public class ViewFight
  {
    public ViewFight() { }

    public ViewFight(int? weather, int fightId, int? location, int? winner, int? loser, DateTime? date, string locationNavigation, string weatherNavigation, string winnerNavigation, string loserNavigation)
    {
      Weather = weather;
      FightId = fightId;
      Location = location;
      Winner = winner;
      Loser = loser;
      StartDate = date;
      EndDate = date;
      LocationNavigation = locationNavigation;
      WeatherNavigation = weatherNavigation;
      WinnerNavigation = winnerNavigation;
      LoserNavigation = loserNavigation;
    }
    //public Fight()
    //{
    //    Comments = new HashSet<Comment>();
    //    Fighters = new HashSet<Fighter>();
    //    Wagers = new HashSet<Wager>();
    //}

    public int FightId { get; set; }
    public int? Winner { get; set; }
    public int? Loser { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Result { get; set; }
    public int? Location { get; set; }
    public int? Weather { get; set; }

    public string WeatherNavigation { get; set; }
    public string LocationNavigation { get; set; }
    public string WinnerNavigation { get; set; }
    public string LoserNavigation { get; set; }


    // public virtual Location LocationNavigation { get; set; }
    //public virtual Character LoserNavigation { get; set; }
    // public virtual Weather WeatherNavigation { get; set; }
    //public virtual Character WinnerNavigation { get; set; }
    //public virtual ICollection<Comment> Comments { get; set; }
    //public virtual ICollection<Fighter> Fighters { get; set; }
    //public virtual ICollection<Wager> Wagers { get; set; }
  }
}
