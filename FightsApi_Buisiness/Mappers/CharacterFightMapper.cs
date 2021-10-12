using FightsApi_Buisiness.Interfaces;
using FightsApi_Buisiness.Repositories;
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Mappers
{
	public class CharacterFightMapper
	{
	//	private CharacterRepository _characterRepo;
		public CharacterFightMapper()
		{
	//		_characterRepo = characterRepo;
		}
		public ViewFightCharacter ModelToViewModel(Fight obj,List<ViewCharacter> cs)
		{
			var fightCharacter = new ViewFightCharacter()
			{
				FightId = obj.FightId,
				StartDate = obj.StartDate,
				EndDate = obj.EndDate,
				//  Result = obj.Result;
				Location = obj.Location,
				Weather = obj.Weather,
				LocationNavigation = obj.LocationNavigation.Location1,
				WeatherNavigation = obj.WeatherNavigation.Description,
				CreatorId = obj.CreatorId,
				PublicFight = obj.Public,
			};
			//fightCharacter.Characters = obj.Fighters.Select(f => _characterRepo.Read(f.CharacterId).Result).ToList();
			fightCharacter.Characters = new ();
			foreach (var c in cs)
			{
				foreach(var f in obj.Fighters){
					if(f.CharacterId==c.CharacterId){
						fightCharacter.Characters.Add(c);
					}
				}
			}
			//fightCharacter.Characters = cs.Find(c=>obj.Fighters.Exists(f=>f.CharacterId == c.CharacterId)).ToList();

			var winningFighter = obj.Fighters.First(f=>f.IsWinner);
			fightCharacter.Winner = fightCharacter.Characters.First(c => c.CharacterId == winningFighter.CharacterId);
			return fightCharacter;
		}

		public List<ViewFightCharacter> ModelToViewModel(List<Fight> obj)
		{
			throw new NotImplementedException();
		}

		public Fight ViewModelToModel(ViewFightCharacter obj)
		{
			throw new NotImplementedException();
		}

		public List<Fight> ViewModelToModel(List<ViewFightCharacter> obj)
		{
			throw new NotImplementedException();
		}
	}
}
