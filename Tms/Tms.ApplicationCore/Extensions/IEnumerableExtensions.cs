using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tms.ApplicationCore.Helpers;

namespace Tms.ApplicationCore.Extensions
{
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Obtains the data as a list; if it is *already* a list, the original object is returned without
		/// any duplication; otherwise, ToList() is invoked.
		/// </summary>
		public static List<T> AsList<T>(this IEnumerable<T> source)
		{
			return (source == null || source is List<T>) ? (List<T>)source : source.ToList();
		}

		/// <summary>
		/// Obtains the data as a HashSet; if it is *already* a HashSet, the original object is returned without
		/// any duplication; otherwise, new HashSet is created.
		/// </summary>
		public static HashSet<T> AsHashSet<T>(this IEnumerable<T> source)
		{
			return (source == null || source is HashSet<T>) ? (HashSet<T>)source : new HashSet<T>(source);
		}

		/// <summary>
		/// Will determine if the two collections contain the same elements.
		/// </summary>
		public static bool All<T>(this IEnumerable<T> source, IEnumerable<T> comparision)
		{
			if (source == comparision)
				return true;

			var list1 = source.AsList();
			var list2 = comparision.AsList();

			if (list1.Count != list2.Count)
				return false;

			foreach (var item in list2)
			{
				if (!list1.Contains(item))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Extension method to call Foreach on each item in a collection.  This exists for List of T, now works with most collections.
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			Check.Null(collection, "collection");
			Check.Null(action, "action", "need to have an action if you want to use this.");
			foreach (var item in collection)
				action(item);
		}

		/// <summary>
		/// Converts a list to a datatable type
		/// </summary>
		public static DataTable ToDataTable<T>(this IEnumerable<T> data)
		{
			var table = new DataTable();
			if (!data.Any())
				return table;

			if (data.First() is IDictionary<string, object>)
			{
				var dictionary = (IDictionary<string, object>)data.First();
				foreach (var key in dictionary.Keys)
				{
					table.Columns.Add(key, typeof(object));
				}

				foreach (var item in data.Cast<IDictionary<string,object>>())
				{
					var row = table.NewRow();
					foreach (var key in dictionary.Keys)
					{
						row[key] = item[key] ?? DBNull.Value;
					}

					table.Rows.Add(row);
				}
			}
			else
			{
				var propertyList = typeof(T).GetProperties().Where(x => !x.PropertyType.Name.StartsWith("c") && x.CanRead).ToList();
				foreach (var prop in propertyList)
				{
					table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
				}

				foreach (var item in data)
				{
					var row = table.NewRow();
					foreach (var prop in propertyList)
					{
						row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
					}

					table.Rows.Add(row);
				}
			}

			if (table.Columns.Count == 0)
				throw new NotImplementedException("No columns were added to the table.  I doubt this is what was intended.");

			return table;
		}
	}
}