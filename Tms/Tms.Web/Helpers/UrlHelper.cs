using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tms.ApplicationCore.Extensions;

namespace Tms.Web.Helpers

{
	public static class UrlHelper
	{
		public static string ConvertToQueryString(object model)
		{
			var dictionary = new RouteValueDictionary(model);
			if (!dictionary.Any())
				return string.Empty;

			var keyValuePairs = new List<string>();

			return dictionary.Keys
				.Where(key => dictionary[key] != null)
				.Select(key =>
				{
					var value = HttpUtility.UrlEncode(dictionary[key].ToString());
					if (dictionary[key] is IEnumerable)
					{
						var list = new List<string>();
						foreach (var item in ((IEnumerable)dictionary[key]))
						{
							list.Add(item.ToString());
						}
						value = list.ToDelimited();
					}

					return HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
				})
				.ToDelimited("&");
		}

		public static string Url<TController>(Expression<Action<TController>> action)
		{
			var method = (System.Reflection.MethodInfo)null;
			var route = action.Body as MethodCallExpression;
			if (route.Method.DeclaringType != typeof(System.Threading.Tasks.Task))
				method = route.Method;
			else
				method = (route.Object as MethodCallExpression).Method;
			var controllerName = typeof(TController).Name;
			controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

			var routeDictionary = new RouteValueDictionary();
			var controllerRouteAttribute = typeof(TController).GetCustomAttributesByType<RouteAttribute>(inherit: false).SingleOrDefault();
			if (controllerRouteAttribute != null)
				routeDictionary.Add("Controller", controllerRouteAttribute.Template == "[controller]" ? controllerName : controllerRouteAttribute.Template);
			else
				routeDictionary.Add("Controller", controllerName);

			var actionRouteAttribute = method.GetCustomAttributesByType<RouteAttribute>(inherit: false).SingleOrDefault();
			if (actionRouteAttribute != null)
				routeDictionary.Add("Action", actionRouteAttribute.Template == "[action]" ? method.Name : actionRouteAttribute.Template);
			else
				routeDictionary.Add("Action", method.Name);

			return "/" + routeDictionary["Controller"] + "/" + routeDictionary["Action"];
		}
	}
}
