using System;

namespace Tms.ApplicationCore.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExcelSheetAttribute : Attribute
	{
		public ExcelSheetAttribute(string sheetName)
		{
			SheetName = sheetName;
			Headers = true;
			FreezeHeaders = true;
			AutoFilter = true;
		}

		/// <summary>
		/// Determines whether the header row is set to AutoFilter.  Default is true.
		/// </summary>
		public bool AutoFilter { get; set; }

		/// <summary>
		/// The name of the sheet.
		/// </summary>
		public string SheetName { get; private set; }

		/// <summary>
		/// Determines whether the header appears.
		/// </summary>
		public bool Headers { get; set; }

		/// <summary>
		/// Determines if the header pane is frozen.
		/// </summary>
		public bool FreezeHeaders { get; set; }

		/// <summary>
		/// Determines the row inside the sheet the data gets written out.
		/// </summary>
		public int StartRow { get; set; }

		/// <summary>
		/// Determines the column inside the sheet the data gets written out.
		/// </summary>
		public int StartColumn { get; set; }

		/// <summary>
		/// Determines whether all properties or only the properties with ExcelColumnAttribute need to be exported. Default is false.
		/// If true properties should not implement ExcelColumnAttribute.
		/// </summary>
		public bool IncludeAllProperties { get; set; }
	}
}
