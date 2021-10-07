using FightsApi_DbContext;
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
		public Task<List<ViewFight>> FindFightsByUserId(int userId);

	}
}
