using System;

namespace Tms.ApplicationCore.Extensions
{
	/// <summary>
	/// Contains the decimal extensions.
	/// </summary>
	public static class DecimalExtensions
	{
		public const string KpmgPercentString = @"#,##0.####\%;(#,##0.####\%);-";
		public const string KpmgCommaString = @"###,##0.####;(###,##0.####);-";
		public const string KpmgCurrencyString = @"$###,##0.####;($###,##0.####);-";
		public const string KpmgCurrencyNoPenniesString = @"$###,##0.####;($###,##0.####);-";
		internal const string DefaultNullValue = "-";
		internal const string MillionRepresentationValValue = "M";
		internal const string ThousandRepresentationValValue = "k";


		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// percent is between [0 - 100]
		/// </summary>
		public static string ToKpmgPercentString(this decimal percent, int decimalPlaces = 0, bool multiplyBy100 = false)
		{
			if (multiplyBy100) percent = percent * 100;
			string s = Math.Round(percent, decimalPlaces).ToString(KpmgPercentString);
			return s;
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// percent is between [0 - 100]
		/// </summary>
		public static string ToKpmgPercentString(this decimal? percent, int decimalPlaces = 0, string nullValue = DefaultNullValue, bool multiplyBy100 = false)
		{
			if (!percent.HasValue)
				return nullValue;

			return percent.Value.ToKpmgPercentString(decimalPlaces: decimalPlaces, multiplyBy100: multiplyBy100);
		}

		/// <summary>
		/// Will format the value using Thousands, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ByThousands(this decimal? decimalValue, int decimalPlaces = 0, string nullValue = DefaultNullValue, string thousandRepresentationVal = ThousandRepresentationValValue)
		{
			if (!decimalValue.HasValue)
				return nullValue;

			return decimalValue.Value.ByThousands(decimalPlaces: decimalPlaces, nullValue: DefaultNullValue, thousandRepresentationVal: thousandRepresentationVal);
		}

		/// <summary>
		/// Will format the value using Thousands, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ByThousands(this decimal decimalValue, int decimalPlaces = 0, string nullValue = DefaultNullValue, string thousandRepresentationVal = ThousandRepresentationValValue)
		{
			var returnValue = Math.Round(decimalValue.SafeDivideBy(1000), decimalPlaces).ToString(KpmgCommaString);
			if (returnValue != DefaultNullValue)
				return returnValue + thousandRepresentationVal;
			else
				return returnValue;
		}

		/// <summary>
		/// Will format the value using Millions, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ByMillions(this decimal? decimalValue, int decimalPlaces = 0, string nullValue = DefaultNullValue, string millionRepresentationVal = MillionRepresentationValValue)
		{
			if (!decimalValue.HasValue)
				return nullValue;

			return decimalValue.Value.ByMillions(decimalPlaces: decimalPlaces, nullValue: DefaultNullValue, millionRepresentationVal: millionRepresentationVal);
		}

		/// <summary>
		/// Will format the value using Millions, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ByMillions(this decimal decimalValue, int decimalPlaces = 0, string nullValue = DefaultNullValue, string millionRepresentationVal = MillionRepresentationValValue)
		{
			var returnValue = Math.Round(decimalValue.SafeDivideBy((long)1000000), decimalPlaces).ToString(KpmgCommaString);
			if (returnValue != DecimalExtensions.DefaultNullValue)
				return returnValue + millionRepresentationVal;
			else
				return returnValue;
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this decimal value, int decimalPlaces = 0)
		{
			string s = Math.Round(value, decimalPlaces).ToString(KpmgCommaString);
			return s;
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative.
		/// </summary>
		public static string ToKpmgCommaString(this decimal? value, int decimalPlaces = 0, string nullValue = Int32Extensions.DefaultNullValue)
		{
			if (value == null)
				return nullValue;

			return ToKpmgCommaString(value.Value, decimalPlaces);
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative with the $ symbol.
		/// </summary>
		public static string ToKpmgCurrencyString(this decimal value, bool includePennies = false, string nullValue = Int32Extensions.DefaultNullValue)
		{
			if (!includePennies)
				return Math.Round(value).ToString(KpmgCurrencyNoPenniesString);

			return Math.Round(value, 2).ToString(KpmgCurrencyString);
		}

		/// <summary>
		/// Will format the value using commas, dashes for 0 and use parenthesis wrappers for negative with the $ symbol.
		/// Default is to strip the pennies.
		/// </summary>
		public static string ToKpmgCurrencyString(this decimal? value, bool includePennies = false, string nullValue = Int32Extensions.DefaultNullValue)
		{
			if (value == null)
				return nullValue;

			return ToKpmgCurrencyString(value.Value, includePennies);
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal? value, decimal? divisor)
		{
			if (!value.HasValue)
				return 0m;
			if (!divisor.HasValue)
				return 0m;
			if (divisor.Value == 0m)
				return 0m;

			return value.Value / divisor.Value;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal value, decimal? divisor)
		{
			if (!divisor.HasValue)
				return 0m;
			if (divisor.Value == 0m)
				return 0m;

			return value / divisor.Value;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal? value, decimal divisor)
		{
			if (!value.HasValue)
				return 0m;
			if (divisor == 0m)
				return 0m;

			return value.Value / divisor;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal value, decimal divisor)
		{
			if (divisor == 0m)
				return 0m;

			return value / divisor;
		}

		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal? value, int divisor)
		{
			return value.SafeDivideBy((decimal)divisor);
		}
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal? value, int? divisor)
		{
			return value.SafeDivideBy((decimal?)divisor);
		}
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal value, int divisor)
		{
			return value.SafeDivideBy((decimal)divisor);
		}
		/// <summary>
		/// Will prevent divide by zero exceptions and instead return 0.
		/// </summary>
		public static decimal SafeDivideBy(this decimal value, int? divisor)
		{
			return value.SafeDivideBy((decimal?)divisor);
		}

	}
}
