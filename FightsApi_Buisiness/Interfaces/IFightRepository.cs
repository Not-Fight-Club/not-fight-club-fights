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

    /// <summary>
    /// read all items the match CreatorID from a table and return a list of those items
    /// </summary>
    /// <param name="obj"> the thing you will use to retrive the object from the database Y indicates it can be whatever type you need</param>
    /// <returns></returns>
    public Task<List<ViewFight>> ReadByCreatorID(Guid obj, bool past);

    public Task<List<ViewFight>> ReadByFightType(bool obj);

    public Task<List<ViewFight>> ReadByCharacterID(int obj);

    public Task<List<ViewFight>> ReadByCharacterCreatorID(int obj);

  }
}
