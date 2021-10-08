using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Mappers
{
  public class VoteMapper : IMapper<Vote, ViewVote>
  {
    public ViewVote ModelToViewModel(Vote obj)
    {
      ViewVote viewVote = new ViewVote();

      viewVote.VoteId = obj.VoteId;
      viewVote.FightId = obj.FightId;
      viewVote.FighterId = obj.FighterId;
      viewVote.UserId = obj.UserId;

      return viewVote;
    }

    public List<ViewVote> ModelToViewModel(List<Vote> obj)
    {
      return obj.ConvertAll(ModelToViewModel);
    }

    public Vote ViewModelToModel(ViewVote obj)
    {
      Vote vote = new Vote();

      vote.VoteId = obj.VoteId;
      vote.FightId = obj.FightId;
      vote.FighterId = obj.FighterId;
      vote.UserId = obj.UserId;

      return vote;
    }

    public List<Vote> ViewModelToModel(List<ViewVote> obj)
    {
      return obj.ConvertAll(ViewModelToModel);
    }
  }
}
