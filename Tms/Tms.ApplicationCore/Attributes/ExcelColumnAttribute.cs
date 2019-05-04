using System;

namespace Tms.ApplicationCore.Attributes
{
	/// <summary>
	/// Marks the placement information on where this property should be placed into excel.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExcelColumnAttribute : Attribute
	{
		public ExcelColumnAttribute()
		{
			AutoFit = true;
		}

		/// <summary>
		/// The order the column should appear in excel.
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// The name for the header. If not supplied, the property name will be used.
		/// </summary>
		public string HeaderName { get; set; }

		/// <summary>
		/// The custom formatter to be applied inside excel.  Use ExcelColumnFormats.Currency for some built in types.
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Determines whether this data is actually a formula.  Only to be used on String data types.
		/// </summary>
		public bool Formula { get; set; }

		/// <summary>
		/// Determines if the column should be auto fit.  Default is true.
		/// </summary>
		public bool AutoFit { get; set; }
	}
}
