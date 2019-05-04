using Tms.ApplicationCore.Models;

namespace Tms.ApplicationCore.Interfaces
{
	/// <summary>
	/// Defines all the configuration endpoints.
	/// </summary>
	public interface ITmsConfiguration
	{
		Settings Settings { get; }
	}
}
