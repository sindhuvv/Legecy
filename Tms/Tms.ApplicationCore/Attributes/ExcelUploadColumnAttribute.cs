using System;

namespace Tms.ApplicationCore.Attributes
{
	/// <summary>
	/// Marks the placement information on where this property should be brought in from excel.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExcelUploadColumnAttribute : Attribute
	{
		public ExcelUploadColumnAttribute()
		{
		}

		/// <summary>
		/// The column from which to import.
		/// </summary>
		public string ColumnLetter { get; set; }
	}
}
