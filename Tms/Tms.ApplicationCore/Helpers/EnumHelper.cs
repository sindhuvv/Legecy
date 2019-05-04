using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tms.ApplicationCore.Attributes;
using Tms.ApplicationCore.Extensions;

namespace Tms.ApplicationCore.Helpers
{
	public static class EnumHelper
	{
		/// <summary>
		/// Will get the enum value by looking at the description attributes.
		/// </summary>
		public static T GetEnumViaDbValue<T>(string value)
		{
			if (String.IsNullOrEmpty(value))
				value = null;

			var enumType = AssertEnumType<T>();
			var returnValue = default(T);
			var valueFound = false;

			foreach (var enumMember in enumType.GetMembers().Where(x => x is System.Reflection.FieldInfo))
			{
				var attributes = enumMember.GetCustomAttributes(typeof(DbValueAttribute), true);
				if (!attributes.Any())
					continue;

				//yeah I know attribute doesn't support duplicates...
				if (attributes.Any(x => ((DbValueAttribute)x).DbValue.Equals(value, StringComparison.OrdinalIgnoreCase)))
				{
					//if value was already found, can't duplicate
					if (valueFound)
						throw new ArgumentException("Found matching description attribute twice for value: " + value + " for type: " + enumType.Name);

					returnValue = (T)Enum.Parse(enumType, enumMember.Name);
					valueFound = true;
				}
			}

			if (!valueFound)
			{
				if (typeof(T).IsGenericType) //assume nullable.
					return default(T);

				throw new ArgumentNullException("Could not find enum value with DbValueAttribute: '" + value + "' for type: " + enumType.Name);
			}

			return returnValue;
		}


        /// <summary>
        /// Will get the enum value's [DbValue] attribute and then the ToString() value of the enum item.
        /// </summary>
        public static string ToDbValueString(this Enum value)
		{
			return GetDbValueString(value);
		}

		/// <summary>
		/// Will get the enum value's [DbValue] attribute and then the ToString() value of the enum item.
		/// </summary>
		public static string GetDbValueString(Enum value)
		{
			if (value == null)
				return null;

			var fieldInfo = value.GetType().GetField(value.ToString());

			if (fieldInfo == null)
				throw new ArgumentException("Could not find enum value: " + value.ToString() + " for type: " + value.GetType().Name);

			var attributes = fieldInfo.GetCustomAttributes(typeof(DbValueAttribute), false);

			if (attributes.Any())
				return ((DbValueAttribute)attributes.First()).DbValue;

			return value.ToString();
		}

		/// <summary>
		/// Will get the enum value's [Description/DbValue] attribute and then the ToString() value of the enum item.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public static string ToDescriptionOrDbValue(this Enum value)
		{
			return GetDescriptionOrDbValue(value);
		}

		/// <summary>
		/// Will get the enum value's [Description/DbValue] attribute and then the ToString() value of the enum item.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public static string GetDescriptionOrDbValue(Enum value)
		{
			if (value == null)
				return null;

			var fieldInfo = value.GetType().GetField(value.ToString());

			if (fieldInfo == null)
				throw new ArgumentException("Could not find enum value: " + value.ToString() + " for type: " + value.GetType().Name);

			var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Any())
				return ((DescriptionAttribute)attributes.First()).Description;

			attributes = fieldInfo.GetCustomAttributes(typeof(DbValueAttribute), false);
			if (attributes.Any())
				return ((DbValueAttribute)attributes.First()).DbValue;

			return value.ToString();
		}

		/// <summary>
		/// Will get the list of all valid values from the enum.
		/// </summary>
		public static IEnumerable<T> GetValues<T>()
		{
			var enumType = AssertEnumType<T>();

			foreach (var enumValue in Enum.GetValues(enumType))
			{
				yield return (T)enumValue;
			}
		}

		/// <summary>
		/// Will take the enum type and sort by the order attribute first.  If the order attribute is missing, it is considered Int32.MaxValue, after which it will sort alphabetically.
		/// </summary>
		public static IReadOnlyList<T> GetEnumValuesInOrder<T>()
		{
			var list = EnumHelper.GetValues<T>();

			//first order by the order attribute, then name.
			return list.OrderBy(x =>
			{
				var fieldInfo = typeof(T).GetField(x.ToString());
				Check.Null(fieldInfo, x.ToString(), "Could not find enum value: " + x.ToString() + " for type: " + typeof(T).Name);

				var attributes = fieldInfo.GetCustomAttributesByType<OrderAttribute>(false);

				if (!attributes.Any())
					return Int32.MaxValue;

				return attributes.Single().Order;
			}).ThenBy(x => x.ToString()).ToList().AsReadOnly();
		}

		/// <summary>
		/// Will check that the Generic is an enumType and return it.
		/// </summary>
		internal static Type AssertEnumType<T>()
		{
			var enumType = typeof(T);

			if (enumType.IsGenericType && enumType.GetGenericArguments().Any() && enumType.GetGenericArguments()[0].IsEnum)
				return enumType.GetGenericArguments()[0];

			if (!enumType.IsEnum)
				throw new NotImplementedException("Can only get enum value from enum type. Type: " + enumType.Name + " is not an enum type.");

			return enumType;
		}
	}
}
