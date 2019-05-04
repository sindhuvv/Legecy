using System;
using System.Collections.Generic;
using System.Linq;
using Tms.Web.Interfaces;

namespace Tms.Web.Models
{
	public class TrainerRole : IBuiltInRole
	{
		public string RoleName
		{
			get { return "Trainers"; }
		}

		public string Description
		{
			get { return "Contains all the trainers."; }
		}

		public bool IsMember(int upn, ICachedSecurityService baseCachedSecurityService)
		{
			//TODO: this is a sample implementation and that is why the hard coding.
			var list = new List<int>() { 2798283, 2809276, 2749371 };
			return list.Any(x => x == upn);
		}
	}
}
