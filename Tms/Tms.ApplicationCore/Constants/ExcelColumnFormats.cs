namespace Tms.ApplicationCore
{
	public static partial class TmsConstants
	{
		/// <summary>
		/// Holds the build in excel custom data format selections.
		/// </summary>
		public static class ExcelColumnFormats
		{
			/// <summary>
			/// Will be displayed as mm/dd/yy.
			/// </summary>
			public const string Date = "m/d/yy;@";

			/// <summary>
			/// Will be displayed as mm/dd.
			/// </summary>
			public const string DateWithoutYear = "m/d;@";

			/// <summary>
			/// Currency with no decimals.   $6,454 | $(3,323) | -
			/// </summary>
			public const string Currency = @"_($* #,##0_);_($* (#,##0);_($* ""-""??_);_(@_)";

			/// <summary>
			/// Currency with decimals.   $6,454.01 | $(3,323.99) | -
			/// </summary>
			public const string CurrencyWithDecimal = @"_($* #,##0.00_);_($* (#,##0.00);_($* ""-""??_);_(@_)";

			/// <summary>
			/// Standard number with no decimals.  2,343 | (2,334) | 0
			/// </summary>
			public const string Number = "#,##0_);(#,##0)";

			/// <summary>
			/// Standard number with decimals.  2,343.45 | (2,334.00) | 0
			/// </summary>
			public const string NumberWithDecimal = "#,##0.00_);(#,##0.00)";

			/// <summary>
			/// Standard percent format .9917 -> 99%
			/// </summary>
			public const string Percent = "0%";

			/// <summary>
			/// Standard percent format .9977 -> 99.77%
			/// </summary>
			public const string PercentWithDecimal = "0.00%";

		}
	}
}
