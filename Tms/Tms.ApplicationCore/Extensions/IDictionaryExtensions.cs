using System.Collections.Generic;

namespace Tms.ApplicationCore.Extensions
{
	public static class IDictionaryExtensions
	{
		/// <summary>
		/// Will look up in the dictionary and see if the key is there.  If so, will return it, otherwise will return null.
		/// This is basically a safe way to use dictionaries when the keys aren't there.
		/// </summary>
		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
		{
			if (dictionary.ContainsKey(key))
				return dictionary[key];

			return null;
		}

		/// <summary>
		/// Will look up in the dictionary and see if the key is there.  If so, will return it, otherwise will return null.
		/// This is basically a safe way to use dictionaries when the keys aren't there.
		/// </summary>
		public static TValue Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
		{
			if (dictionary.ContainsKey(key))
				return dictionary[key];

			return null;
		}
	}
}
