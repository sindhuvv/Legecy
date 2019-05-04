using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using Tms.ApplicationCore.Extensions;

namespace Tms.Web.Extensions
{
	public static class RouteValueDictionaryExtensions
	{
		/// <summary>
		/// Returns URL query string by processing the RouteValueDictionary.
		/// </summary>
		public static string ToQueryString(this RouteValueDictionary dictionary)
		{
			if (!dictionary.Any())
				return String.Empty;

			return dictionary.Keys.Where(key => dictionary[key] != null)
				.Select(key =>
				{
					var value = dictionary[key] is IEnumerable ?
						((IEnumerable)dictionary[key]).Cast<object>().Select(x => x.ToString()).ToDelimited() :
						dictionary[key].ToString();

					return HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
				})
				.ToDelimited("&");
		}
	}
}