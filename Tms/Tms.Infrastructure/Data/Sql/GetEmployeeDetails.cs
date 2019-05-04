namespace Tms.Infrastructure.Data
{
	public partial class Sql
	{
		public const string GetEmployeeUpnByDomainLogin = @"
		SELECT Upn FROM [dbo].[Employee] WHERE DomainLogin = @DomainLogin;";

		public const string GetEmployeeDetailsByHRName = @"
		SELECT * FROM [dbo].[Employee] WHERE HRName LIKE '%'+@Searchvalue+'%';";

		public const string GetEmployeeDetailsByUpn = @"
		SELECT * FROM [dbo].[Employee] WHERE UPN = @UPN;";
	}


}