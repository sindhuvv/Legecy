namespace Tms.Web.Interfaces
{
	public interface IBuiltInRole
	{
		string RoleName { get; }

		string Description { get; }

		bool IsMember(int upn, ICachedSecurityService _cachedServiceService);
	}
}
