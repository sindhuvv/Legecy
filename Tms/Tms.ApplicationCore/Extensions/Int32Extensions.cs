using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.ApplicationCore.Extensions
{
	/// <summary>
	/// extension methods for integers
	/// </summary>
	public static class Int32Extensions
	{
		internal const string DefaultNullValue = "-";
		internal const string ToDelimited_DefaultDelimiter = ",";

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this int value)
		{
			return value.ToString("#,##0;(#,##0);-");
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this int? value, string nullValue = DefaultNullValue)
		{
			if (value == null)
				return nullValue;

			return value.Value.ToKpmgCommaString();
		}

		/// <summary>
		/// Will convert the nullable to the integer value or zero if null.
		/// </summary>
		public static int ToInt32OrZero(this int? value)
		{
			return value ?? 0;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this int? value, decimal divisor)
		{
			return ((decimal?)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this int? value, decimal? divisor)
		{
			return ((decimal?)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static int SafeDivideBy(this int? value, int divisor)
		{
			return value.SafeDivideBy((int?)divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static int SafeDivideBy(this int? value, int? divisor)
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
		public static decimal SafeDivideBy(this int value, decimal divisor)
		{
			return ((decimal)value).SafeDivideBy(divisor);
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this int value, decimal? divisor)
		{
			return ((decimal)value).SafeDivideBy(divisor);
		}
		
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static int SafeDivideBy(this int value, int divisor)
		{
			return ((int?)value).SafeDivideBy((int?)divisor);
		}
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static int SafeDivideBy(this int value, int? divisor)
		{
			return ((int?)value).SafeDivideBy(divisor);
		}

		/// <summary>
		/// Will simply perform a string join on the list of strings with the delimiter.  The default is ",".
		/// It does not protect any element with conflicts with the delimiter.
		/// </summary>
		public static string ToDelimited(this IEnumerable<int> values, string delimiter = ToDelimited_DefaultDelimiter)
		{
			if (values == null)
				return String.Empty;

			return String.Join(delimiter, values.ToArray());
		}
	}
}
