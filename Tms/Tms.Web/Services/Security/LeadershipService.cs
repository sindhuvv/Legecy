using System.Collections.Generic;
using Tms.Web.Interfaces;
using Tms.Web.Models;

namespace Tms.Web.Services
{
	public class LeadershipService : ILeadershipService
	{
		#region static
		private static IReadOnlyList<IBuiltInRole> _builtInRoles;

		static LeadershipService()
		{
			RegisterBuiltInRoles();
		}

		private static void RegisterBuiltInRoles()
		{
			_builtInRoles = new List<IBuiltInRole>
			{
				new TrainerRole()
			};
		}
		#endregion

		public IReadOnlyList<IBuiltInRole> GetBuiltInRoles()
		{
			return _builtInRoles;
		}
	}
}
