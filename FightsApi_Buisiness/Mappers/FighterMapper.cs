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
  public class FighterMapper : IMapper<Fighter, ViewFighter>
  {
    public ViewFighter ModelToViewModel(Fighter obj)
    {
      ViewFighter viewFighter = new ViewFighter();
      viewFighter.FighterId = obj.FighterId;
      viewFighter.FightId = obj.FightId;
      viewFighter.CharacterId = obj.CharacterId;
      //  viewFighter.Votes = obj.Votes;

      viewFighter.IsWinner = obj.IsWinner;
      return viewFighter;
    }

    public Fighter ViewModelToModel(ViewFighter obj)
    {
      Fighter fighter = new Fighter();
      fighter.FighterId = obj.FighterId;
    //  fighter.FightId = obj.FightId;
   //   fighter.CharacterId = obj.CharacterId;
     // fighter.Votes = obj.Votes;

      return fighter;
    }

    public List<ViewFighter> ModelToViewModel(List<Fighter> obj)
    {
      List<ViewFighter> fighters = new List<ViewFighter>();
      for (int i = 0; i < obj.Count; i++)
      {
        ViewFighter f = new ViewFighter();
        f.FighterId = obj[i].FighterId;
        f.FightId = obj[i].FightId;
        f.CharacterId = obj[i].CharacterId;
        //    f.Votes = obj[i].Votes;
        f.IsWinner = obj[i].IsWinner;
        fighters.Add(f);
      }

      return fighters;
    }

    public List<Fighter> ViewModelToModel(List<ViewFighter> obj)
    {
      List<Fighter> fighters = new List<Fighter>(obj.Count);
      for (int i = 0; i < obj.Count; i++)
      {
        fighters[i].FighterId = obj[i].FighterId;
       // fighters[i].FightId = obj[i].FightId;
    //    fighters[i].CharacterId = obj[i].CharacterId;
      //  fighters[i].Votes = obj[i].Votes;
      }

      return fighters;
    }
  }
}
