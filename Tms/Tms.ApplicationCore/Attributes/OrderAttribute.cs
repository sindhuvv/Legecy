using Tms.ApplicationCore.Helpers;
using System;

namespace Tms.ApplicationCore.Attributes
{
	/// <summary>
	/// Attribute used for controlling the sort order.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class OrderAttribute : Attribute
	{
		/// <summary>
		/// The order must be positive.
		/// </summary>
		public OrderAttribute(int order)
		{
			Check.False(order > 0, "Order must be a positive int");
			Order = order;
		}

		/// <summary>
		/// The order this enum value should be sorted by, must be positive.
		/// </summary>
		public int Order { get; private set; }
	}
}
