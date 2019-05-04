using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tms.ApplicationCore.Helpers;

namespace Tms.ApplicationCore.Extensions
{
	public static class StringExtensions
	{
		internal const string ToDelimited_DefaultDelimiter = ",";
		internal const string FromDelimited_DefaultDelimiter = ",";

		public static string Truncate(this string value, int maxChars, string text = null)
		{
			if (string.IsNullOrWhiteSpace(value)) return string.Empty;
			return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "..." + text;
		}

		public static string ToYesNoString(this string value)
		{
			if (value.EqualsOrdinalIgnoreCase(TmsConstants.NA))
				return TmsConstants.NA;
			if (value.EqualsOrdinalIgnoreCase(TmsConstants.True))
				return TmsConstants.Yes;
			if (value.EqualsOrdinalIgnoreCase(TmsConstants.False))
				return TmsConstants.No;
			return null;
		}

		/// <summary>
		/// Returns true if the text body is empty. Can be used safely on null values of string.
		/// </summary>
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// Determines if the two strings are equal comparing them by ordinal ignore case StringComparision options.
		/// </summary>
		public static bool EqualsOrdinalIgnoreCase(this string value1, string value2)
		{
			return string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);
		}


		/// <summary>
		/// Will simply perform a string join on the list of strings with the delimiter.  The default is ",".
		/// It does not protect any element with conflicts with the delimiter.
		/// </summary>
		public static string ToDelimited(this IEnumerable<string> values, string delimiter = ToDelimited_DefaultDelimiter)
		{
			if (values == null)
				return string.Empty;

			return string.Join(delimiter, values.ToArray());
		}

		/// <summary>
		/// Will take the string and split it based on the delimiter.
		/// </summary>
		public static List<string> FromDelimited(this string value, string delimiter = FromDelimited_DefaultDelimiter)
		{
			if (string.IsNullOrEmpty(value))
				return new List<string>();

			return value.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		/// <summary>
		/// Will take the string and split it based on the delimiter and convert it to the enum value.
		/// </summary>
		public static List<T> FromDelimitedToEnum<T>(this string value, string delimiter = FromDelimited_DefaultDelimiter)
		{
			EnumHelper.AssertEnumType<T>();

			var list = value.FromDelimited(delimiter);

			return list.Select(x => EnumHelper.GetEnumViaDbValue<T>(x)).ToList();
		}

		/// <summary>
		/// Will take the string and split it based on the delimiter and convert it to the integer value.
		/// </summary>
		public static List<int> FromDelimitedToInt32(this string value, string delimiter = FromDelimited_DefaultDelimiter)
		{
			var list = value.FromDelimited(delimiter);
			return list.Select(x => int.Parse(x)).ToList();
		}

		/// <summary>
		/// Will strip all non-alphanumeric characters and replaces spaces with '-' all in lower case.
		/// </summary>
		public static string ToUrlSafeFragment(this string value)
		{
			if (value == null)
				return string.Empty;

			return _urlSafeFragmentRegex.Replace(value, "").Replace(" ", "-").ToLower();
		}

		private static Regex _urlSafeFragmentRegex = new Regex(@"[^A-Za-z0-9 ]+", RegexOptions.Compiled);
	}
}
