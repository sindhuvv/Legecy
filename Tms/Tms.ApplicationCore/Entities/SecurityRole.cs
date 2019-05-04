namespace Tms.ApplicationCore.Entities
{
	public class SecurityRole : BaseEntity
	{
		public string Practice { get; set; }

		public string RoleName { get; set; }

		public int PermFlag { get; set; }

		public string Description { get; set; }

		public bool IsBuiltIn { get; set; }
	}
}