using System;

namespace Tms.ApplicationCore.Extensions
{
	public static class BooleanExtensions
	{
		internal const string DefaultTrueString = "Yes";
		internal const string DefaultFalseString = "No";
		internal const string DefaultNullString = "N/A";

		/// <summary>
		/// Will convert the bool value to a string value.  Default is Yes/No.
		/// </summary>
		public static string ToYesNoString(this bool value, string trueString = DefaultTrueString, string falseString = DefaultFalseString)
		{
			return ToYesNoString((bool?)value, trueString, falseString);
		}

		/// <summary>
		/// Will convert the bool value to a string value.  Default is Yes/No or N/A for nulls.
		/// </summary>
		public static string ToYesNoString(this bool? value, string trueString = DefaultTrueString, string falseString = DefaultFalseString, string nullString = DefaultNullString)
		{
			if (!value.HasValue)
				return nullString;

			if (value.Value)
				return trueString;

			return falseString;
		}
	}
}
