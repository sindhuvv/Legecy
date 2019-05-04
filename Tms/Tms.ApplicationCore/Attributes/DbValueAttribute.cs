using Tms.ApplicationCore.Helpers;
using System;

namespace Tms.ApplicationCore.Attributes
{
	/// <summary>
	/// Attribute class used to defined what the database values are for an enum.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class DbValueAttribute : Attribute
	{
		/// <summary>
		/// The value in the database for the enum value.
		/// </summary>
		public DbValueAttribute(string dbValue)
		{
			Check.Null(dbValue, "dbValue", "The database value must have a value.");

			DbValue = dbValue;
		}

		/// <summary>
		/// The string value stored inside the database.
		/// </summary>
		public string DbValue { get; private set; }
	}
}
