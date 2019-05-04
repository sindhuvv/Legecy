using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Helpers;
using Tms.Web.Helpers;

namespace Tms.Web.Extensions
{
	public static class DDLExtensions
	{
		#region MarkSelected
		/// <summary>
		/// Clears the existing selections and selects the values exists in selectedValues parameter
		/// </summary>
		public static IEnumerable<SelectListItem> MarkSelected(this IEnumerable<SelectListItem> list, List<string> selectedValues)
		{
			list.ForEach(x => x.MarkSelected(selectedValues));
			return list;
		}

		/// <summary>
		/// Clears the existing selections and selects the values exists in selectedValues parameter
		/// </summary>
		public static IEnumerable<SelectListItem> MarkSelected(this IEnumerable<SelectListItem> list, List<int> selectedValues)
		{
			var selectedStringValues = selectedValues == null ? null : selectedValues.Select(x => x.ToString()).AsList();

			list.ForEach(x => x.MarkSelected(selectedStringValues));
			return list;
		}

		/// <summary>
		/// Clears the existing selections and selects the value equal to selectedValue parameter
		/// </summary>
		public static IEnumerable<SelectListItem> MarkSelected(this IEnumerable<SelectListItem> list, string selectedValue)
		{
			list.ForEach(x => x.MarkSelected(selectedValue));
			return list;
		}

		/// <summary>
		/// Marks the item as selected if the selectedValue parameter equals item value
		/// </summary>
		public static SelectListItem MarkSelected(this SelectListItem item, string selectedValue)
		{
			if (item != null)
				item.Selected = !string.IsNullOrWhiteSpace(selectedValue) && selectedValue.EqualsOrdinalIgnoreCase(item.Value);
			return item;
		}

		/// <summary>
		/// Marks the item as selected if the selectedValues parameter contains the item value
		/// </summary>
		public static SelectListItem MarkSelected(this SelectListItem item, IEnumerable<string> selectedValues)
		{
			if (item != null)
				item.Selected = selectedValues != null && selectedValues.Contains(item.Value, StringComparer.OrdinalIgnoreCase);
			return item;
		}
		#endregion

		#region ToSelectListItems
		public static List<SelectListItem> ToSelectListItems(this IEnumerable<int> values, string selectedValue = null, bool addDefault = false)
		{
			Check.Null(values, "values");

			var query = string.IsNullOrWhiteSpace(selectedValue) ? values.Select(x => DDLHelper.GetSelectListItem(x)) :
				values.Select(x => DDLHelper.GetMarkedSelectListItem(x, selectedValue));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}

		public static List<SelectListItem> ToSelectListItems(this IEnumerable<string> values, string selectedValue = null, bool addDefault = false)
		{
			Check.Null(values, "values");

			var query = string.IsNullOrWhiteSpace(selectedValue) ? values.Select(x => DDLHelper.GetSelectListItem(x)) :
				values.Select(x => DDLHelper.GetMarkedSelectListItem(x, selectedValue));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}

		public static List<SelectListItem> ToSelectListItems(this IEnumerable<int> values, List<string> selectedValues = null, bool addDefault = false)
		{
			Check.Null(values, "values");

			var query = selectedValues == null || !selectedValues.Any() ? values.Select(x => DDLHelper.GetSelectListItem(x)) :
				values.Select(x => DDLHelper.GetMarkedSelectListItem(x, selectedValues));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}

		public static List<SelectListItem> ToSelectListItems(this IEnumerable<string> values, List<string> selectedValues = null, bool addDefault = false)
		{
			Check.Null(values, "values");

			var query = selectedValues == null || !selectedValues.Any() ? values.Select(x => DDLHelper.GetSelectListItem(x)) :
				values.Select(x => DDLHelper.GetMarkedSelectListItem(x, selectedValues));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}
		#endregion

		#region ToDefaultPrependedList
		/// <summary>
		/// Returns a new list which will contain the default item and the items of the current list
		/// </summary>
		public static List<SelectListItem> ToDefaultPrependedList(this IEnumerable<SelectListItem> list, string defaultText = null)
		{
			Check.Null(list, "list");

			var currentList = list.AsList();
			var newList = new List<SelectListItem>(currentList.Count + 1) {
				DDLHelper.GetDefaultSelectListItem(defaultText)
			};

			newList.AddRange(currentList);
			return newList;
		}
		#endregion
	}
}