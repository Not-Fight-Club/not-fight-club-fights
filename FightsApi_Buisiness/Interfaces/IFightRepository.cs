using FightsApi_Data;
using FightsApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Buisiness.Interfaces
{
public interface IFightRepository: IRepository<ViewFight, int>
	{
		//find fights by user ID
		public Task<List<ViewFightCharacter>> FindFightsByUserId(Guid userId);

	}
}
