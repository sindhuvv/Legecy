using System;

namespace Tms.ApplicationCore.Extensions
{
	/// <summary>
	/// Class containing date time helper functions.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// {0:MM/dd/yy} used in TextBoxFor attributes.
		/// </summary>
		public const string KpmgShortDateStringFormat = "{0:" + KpmgShortDateStringToStringFormat + "}";
		internal const string KpmgShortDateStringToStringFormat = "MM/dd/yy";
		internal const string DefaultNullValue = "-";
		private static readonly DateTime _unixTime = new DateTime(1970, 1, 1);

		/// <summary>
		/// Will format the date like "10/20/13"
		/// </summary>
		public static string ToKpmgShortDateString(this DateTime dateTime)
		{
			return dateTime.ToString(KpmgShortDateStringToStringFormat);
		}

		/// <summary>
		/// Will format the date like "10/20/13".  When null will be "-" unless passed in to method.
		/// </summary>
		public static string ToKpmgShortDateString(this DateTime? dateTime, string nullValue = DefaultNullValue)
		{
			if (dateTime == null)
				return nullValue;

			return dateTime.Value.ToKpmgShortDateString();
		}

		/// <summary>
		/// Will take the date and move to the first day of the month.
		/// </summary>
		public static DateTime ToFirstDayOfMonth(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		/// <summary>
		/// Will take the date and move to the last day of the month.
		/// </summary>
		public static DateTime ToLastDayOfMonth(this DateTime dateTime)
		{
			return dateTime.ToFirstDayOfMonth().AddMonths(1).AddDays(-1);
		}

		/// <summary>
		/// Will return the Oct 1 date for the appropriate year for fiscal year beginning.
		/// The years adjust are to move forward or backward the number of years.
		/// </summary>
		public static DateTime ToBeginFiscalYear(this DateTime dateTime, int yearsAdjust = 0)
		{
			dateTime = dateTime.ToFirstDayOfMonth();
			while (dateTime.Month != 10)
				dateTime = dateTime.AddMonths(-1);

			return dateTime.AddYears(yearsAdjust);
		}

		/// <summary>
		/// Will return the Sept 30 date for the appropriate year for the end of the fiscal year.
		/// The years adjust are to move forward or backward the number of years.
		/// </summary>
		public static DateTime ToEndFiscalYear(this DateTime dateTime, int yearsAdjust = 0)
		{
			dateTime = dateTime.ToBeginFiscalYear(1).AddDays(-1);

			return dateTime.AddYears(yearsAdjust);
		}

		/// <summary>
		/// Will return beginning date for that year for given date.
		/// The years adjust are to move forward or backward the number of years.
		/// </summary>
		public static DateTime ToBeginCalendarYear(this DateTime dateTime, int yearsAdjust = 0)
		{
			var date = new DateTime(dateTime.Year, 1, 1);
			return date.AddYears(yearsAdjust);
		}

		/// <summary>
		/// Will return last date in that year for given date.
		/// The years adjust are to move forward or backward the number of years.
		/// </summary>
		public static DateTime ToEndCalendarYear(this DateTime dateTime, int yearsAdjust = 0)
		{
			var date = new DateTime(dateTime.Year, 12, 31);
			return date.AddYears(yearsAdjust);
		}

		/// <summary>
		/// Will convert the DateTime to a javascript command to create a data object inside javascript.
		/// </summary>
		public static string ToJavascriptDate(this DateTime dateTime)
		{
			if (dateTime < _unixTime)
				throw new NotImplementedException("date must be after jan 1, 1970");
			return "new Date(" + (dateTime.Subtract(_unixTime).TotalMilliseconds)  + ")";
		}
		/// <summary>
		/// Returns first day of the week
		/// </summary>
		public static DateTime ToFirstDayOftheWeek(this DateTime dateTime)
		{
			return dateTime.AddDays(-(int)dateTime.DayOfWeek);
		}
		/// <summary>
		/// Returns last day of the week
		/// </summary>
		public static DateTime ToLastDayOftheWeek(this DateTime dateTime)
		{
			return dateTime.AddDays(6 - (int)dateTime.DayOfWeek);
		}
		/// <summary>
		/// Returns a Javascript Date type
		/// @Model.Endate = new Datetime(2005, 2, 5)
		/// var jsVariable = @Model.EndDate.ToDateOnlyJavascript();
		/// var jsVariable = new Date(2005,2,5);
		/// </summary>
		public static string ToDateOnlyJavascript(this DateTime dateTime)
		{
			if (dateTime < _unixTime)
				throw new NotImplementedException("date must be after jan 1, 1970");
			return String.Format("new Date({0}, {1}, {2})", dateTime.Year, dateTime.Month - 1, dateTime.Day);
		}
		/// <summary>
		/// Returns a Javascript Date type (will return null if value passed is null)
		/// @Model.Endate = new Datetime(2005, 2, 5)
		/// var jsVariable = @Model.EndDate.ToDateOnlyJavascript();
		/// var jsVariable = new Date(2005,2,5);
		/// var jsVariable = null;
		/// </summary>
		public static string ToDateOnlyJavascript(this DateTime? dateTime)
		{
			if (dateTime.HasValue)
				return dateTime.Value.ToDateOnlyJavascript();
			return "null";
		}
	}
}
