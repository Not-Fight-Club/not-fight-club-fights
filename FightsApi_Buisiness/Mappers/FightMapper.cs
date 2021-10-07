using FightsApi_Buisiness.Interfaces;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Logic.Mappers
{
  public class FightMapper : IMapper<Fight, ViewFight>
  {
    public ViewFight ModelToViewModel(Fight obj)
    {
      ViewFight viewFight = new ViewFight();
      viewFight.FightId = obj.FightId;
    //  viewFight.Winner = obj.Winner;
    //  viewFight.Loser = obj.Loser;
      viewFight.StartDate = obj.StartDate;
      viewFight.EndDate = obj.EndDate;
    //  viewFight.Result = obj.Result;
      viewFight.Location = obj.Location;
      viewFight.Weather = obj.Weather;

      return viewFight;
    }


    public Fight ViewModelToModel(ViewFight obj)
    {
      Fight fight = new Fight();
      fight.FightId = obj.FightId;
    //  fight.Winner = obj.Winner;
    //  fight.Loser = obj.Loser;
      fight.StartDate = obj.StartDate;
      fight.EndDate = obj.EndDate;
    //  fight.Result = obj.Result;
      fight.Location = obj.Location;
      fight.Weather = obj.Weather;

      return fight;
    }

    public List<Fight> ViewModelToModel(List<ViewFight> obj)
    {
      throw new NotImplementedException();
    }


    public List<ViewFight> ModelToViewModel(List<Fight> obj)
    {
      return obj.ConvertAll(ModelToViewModel);
      //use lazy loading to call the weather description to be loaded
      List<ViewFight> fights = new List<ViewFight>();
      for (int i = 0; i < obj.Count; i++)
      {
        /*
        ViewFight f = new ViewFight(
        obj[i].Weather,
        obj[i].FightId,
        obj[i].Location,
        obj[i].Winner,
        obj[i].Loser,
        obj[i].StartDate,
        obj[i].EndDate,
        obj[i].LocationNavigation.Location1,
        obj[i].WeatherNavigation.Description,
        obj[i].WinnerNavigation.Name,
        obj[i].LoserNavigation.Name
        
        );
        fights.Add(f);
        */
      }
      return fights;
    }
  }
}
