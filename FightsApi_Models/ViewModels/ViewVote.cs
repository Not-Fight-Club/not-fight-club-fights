using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.ViewModels
{
  public class ViewVote
  {
    public int VoteId { get; set; }
    public int FightId { get; set; }
    public int FighterId { get; set; }
    public Guid UserId { get; set; }

  }
}
