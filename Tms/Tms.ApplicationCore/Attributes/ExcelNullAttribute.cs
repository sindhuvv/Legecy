using System;

namespace Tms.ApplicationCore.Attributes
{
	/// <summary>
	/// Attribute to be used with excel model binding when the value is null and a value other than 'BLANK' is placed into the cell.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExcelNullAttribute : Attribute
	{
		/// <summary>
		/// The value that should be placed into the cell when the source is null.
		/// </summary>
		public ExcelNullAttribute(string nullValue)
		{
			NullValue = nullValue;
		}

		/// <summary>
		/// The nullValue of the attribute.
		/// </summary>
		public string NullValue { get; private set; }
	}
}
