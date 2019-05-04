using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tms.ApplicationCore.Extensions
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Gets a single attribute by type.
		/// </summary>
		public static TAttributeType GetCustomAttributeByType<TAttributeType>(this Type type, bool inherit = true) where TAttributeType : Attribute
		{
			var attributes = type.GetCustomAttributesByType<TAttributeType>(inherit);
			if (attributes.Count() > 1)
				throw new ArgumentException("GetCustomAttributeByType: expected a single result, but actually found too many elements in the result set.");

			return attributes.SingleOrDefault();
		}

		/// <summary>
		/// Will get the list of custom attributes, non-generic version.
		/// </summary>
		public static IEnumerable<Attribute> GetCustomAttributesByType(this Type type, Type attributeType, bool inherit = true)
		{
			var attributes = type.GetCustomAttributes(attributeType, inherit);

			var strongAttributes = new List<Attribute>();
			Array.ForEach(attributes, x => strongAttributes.Add((Attribute)x));

			return strongAttributes;
		}

		/// <summary>
		/// Will get the list of custom attributes for the type.
		/// </summary>
		public static IEnumerable<T> GetCustomAttributesByType<T>(this Type type, bool inherit = true)
						where T : Attribute
		{
			var attributes = type.GetCustomAttributes(typeof(T), inherit);

			var strongAttributes = new List<T>();
			Array.ForEach(attributes, x => strongAttributes.Add((T)x));

			return strongAttributes;
		}

		/// <summary>
		/// Will get the list of custom attributes for the member info.
		/// </summary>
		public static IEnumerable<T> GetCustomAttributesByType<T>(this MemberInfo memberInfo, bool inherit = true)
						where T : Attribute
		{
			var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);

			var strongAttributes = new List<T>();
			Array.ForEach(attributes, x => strongAttributes.Add((T)x));

			return strongAttributes;
		}

	}
}