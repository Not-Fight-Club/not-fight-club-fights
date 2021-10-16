using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Interfaces
{
  public interface IVoteRepository : IRepository<ViewVote, int>
  {
    /// <summary>
    /// Get list of votes for a particular fighter in a particular fight.
    /// </summary>
    /// <param name="fightId"></param>
    /// <param name="fighterId"></param>
    /// <returns></returns>
    public Task<ViewVote[]> ReadbyChoice(int fightId, int fighterId);
  }
}
