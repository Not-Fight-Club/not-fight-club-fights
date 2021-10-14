using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.ViewModels
{
	public class ViewFightCharacter:ViewFight
	{
		//characters for each fight 
		public List<ViewCharacter> Characters { get; set; } = new List<ViewCharacter>();
		//winner object
		public ViewCharacter Winner { get; set;  }

	}
}
