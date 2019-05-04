namespace Tms.Infrastructure.Data
{
	public partial class Sql
	{
		public const string GetSecurityRoleDetails = @"
		SELECT * FROM [dbo].[SecurityRole]

		SELECT roleUser.* FROM [dbo].[SecurityUserRole] roleUser
		INNER JOIN [dbo].[SecurityRole] role ON roleUser.SecurityRoleId = role.Id
	";
	}
}