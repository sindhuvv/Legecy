using System.Collections.Generic;
using System.Linq;

namespace Tms.ApplicationCore.Extensions
{
	/// <summary>
	/// extension methods for integers
	/// </summary>
	public static class Int64Extensions
	{
		internal const string DefaultNullValue = "-";
		internal const string ToDelimited_DefaultDelimiter = ",";

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this long value)
		{
			return value.ToString("#,##0;(#,##0);-");
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this long? value, string nullValue = DefaultNullValue)
		{
			if (value == null)
				return nullValue;

			return value.Value.ToKpmgCommaString();
		}

		/// <summary>
		/// Will convert the nullable to the integer value or zero if null.
		/// </summary>
		public static long ToInt64OrZero(this long? value)
		{
			return value ?? 0;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this long? value, decimal divisor)
		{
			return ((decimal?)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this long? value, decimal? divisor)
		{
			return ((decimal?)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static long SafeDivideBy(this long? value, long divisor)
		{
			return value.SafeDivideBy((long?)divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static long SafeDivideBy(this long? value, long? divisor)
		{
			if (!value.HasValue)
				return 0;
			if (!divisor.HasValue)
				return 0;
			if (divisor.Value == 0)
				return 0;

			return value.Value / divisor.Value;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this long value, decimal divisor)
		{
			return ((decimal)value).SafeDivideBy(divisor);
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this long value, decimal? divisor)
		{
			return ((decimal)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static long SafeDivideBy(this long value, long divisor)
		{
			return ((long?)value).SafeDivideBy((long?)divisor);
		}
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static long SafeDivideBy(this long value, long? divisor)
		{
			return ((long?)value).SafeDivideBy(divisor);
		}

		/// <summary>
		/// Will simply perform a string join on the list of strings with the delimiter.  The default is ",".
		/// It does not protect any element with conflicts with the delimiter.
		/// </summary>
		public static string ToDelimited(this IEnumerable<long> values, string delimiter = ToDelimited_DefaultDelimiter)
		{
			if (values == null)
				return string.Empty;

			return string.Join(delimiter, values.ToArray());
		}
	}
}
