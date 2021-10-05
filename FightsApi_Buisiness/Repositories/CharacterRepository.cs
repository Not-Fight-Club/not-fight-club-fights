using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Models.ViewModels;

namespace FightsApi_Buisiness.Repositories
{
	public class CharacterRepository : IRepository<ViewCharacter, int>
	{
		public Task<ViewCharacter> Add(ViewCharacter obj)
		{
			throw new NotImplementedException();
		}

		public Task<ViewCharacter> Read(int obj)
		{
			// Access microservice for characters
			throw new NotImplementedException();
		}

		public Task<List<ViewCharacter>> Read()
		{
			throw new NotImplementedException();
		}

		public Task<ViewCharacter> Update(ViewCharacter obj)
		{
			throw new NotImplementedException();
		}
	}
}
