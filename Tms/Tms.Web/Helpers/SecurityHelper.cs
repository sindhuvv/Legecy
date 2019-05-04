using System;
using System.Collections.Generic;
using System.Linq;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Models;

namespace Tms.Web.Helpers
{
	public static class SecurityHelper
	{
		public static List<Permissions> GetPermFlags(Permissions permflags)
		{
			var perms = new List<Permissions>();

			Enum.GetValues(typeof(Permissions)).Cast<Permissions>().ForEach
			(
				p => { if (permflags.HasFlag(p)) { perms.Add(p); } }
			);
			return perms;
		}

		public static List<Permissions> GetPermFlags(int permflags)
		{
			var perms = new List<Permissions>();
			var vals = Enum.GetValues(typeof(Permissions));
			foreach (var p in vals)
			{
				if ((permflags & ((int)p)) != 0)
					perms.Add((Permissions)p);
			}
			return perms;
		}

		public static int GetPerm(List<Permissions> permissions)
		{
			int result = 0;
			for (int i = 0; i < permissions.Count; ++i)
			{
				result ^= (int)permissions[i];
			}
			return result;
		}
	}
}
