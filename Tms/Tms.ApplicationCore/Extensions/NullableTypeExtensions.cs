namespace Tms.ApplicationCore.Extensions
{
	public static class NullableTypeExtensions
	{
		/// <summary>
		/// Returns true if the object is not null and the value of the object equals to the valueToCompare, else returns false.
		/// </summary>
		public static bool HasValueAndEquals<T>(this T? obj, T valueToCompare) where T : struct
		{
			return obj.HasValue && obj.Value.Equals(valueToCompare);
		}
	}
}