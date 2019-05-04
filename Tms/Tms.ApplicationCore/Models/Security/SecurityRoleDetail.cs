using System.Collections.Generic;

namespace Tms.ApplicationCore.Models
{
	public class SecurityRoleDetail
	{
		public List<int> Upns { get; set; }

		public int Id { get; set; }

		public string Practice { get; set; }

		public string RoleName { get; set; }

		public string Description { get; set; }

		public bool IsBuiltIn { get; set; }

		public int PermFlag { get; set; }
	}
}