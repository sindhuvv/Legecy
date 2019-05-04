namespace Tms.ApplicationCore.Entities
{
	public class SecurityUserRole : BaseEntity
	{
		public int Upn { get; set; }

		public int SecurityRoleId { get; set; }
	}
}