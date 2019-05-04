using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Helpers;
using Tms.Web.Extensions;

namespace Tms.Web.Helpers
{
	public static class DDLHelper
	{
		internal const string GetYears_StartYearOutOfRange = "Start year should be earlier than end year.";
		internal const string GetYears_ArgumentOutOfRange = "Start or End year cannot be negative.";

		#region Private Methods
		private static IOrderedEnumerable<string> GetDbValueStringOrderedListQuery(Type enumType)
		{
			return Enum.GetValues(enumType).Cast<Enum>().Select(x => x.ToDbValueString()).OrderBy(x => x);
		}
		#endregion

		#region GetMonths
		/// <summary>
		/// Will get a drop down list containing months as selection.  The Value property will be the first of the month.
		/// It will range out  
		/// </summary>
		/// <param name="startDate">The start date for selection.  Default is today.</param>
		/// <param name="months">The number of months forward the values should be based on startDate.  If negative, will list dates backwards.</param>
		/// <param name="displayFormat">The display format for the Text value.  Default is 'MMM yyyy'.</param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> GetMonths(DateTime? startDate = null, int months = 12, string displayFormat = "MMM yyyy")
		{
			if (!startDate.HasValue)
				startDate = DateTime.Now;

			var current = startDate.Value.ToFirstDayOfMonth();
			var last = current.AddMonths(months);
			var list = new List<SelectListItem>();

			if (months > 0)
			{
				while (current <= last)
				{
					list.Add(new SelectListItem()
					{
						Text = current.ToString(displayFormat),
						Value = current.ToString(),
					});

					current = current.AddMonths(1);
				}
			}
			else
			{
				while (current >= last)
				{
					list.Add(new SelectListItem()
					{
						Text = current.ToString(displayFormat),
						Value = current.ToString(),
					});

					current = current.AddMonths(-1);
				}
			}


			return list;

		}
		#endregion

		#region GetYears
		/// <summary>
		/// Will get the drop down list of all the years between the range specified.
		/// </summary>
		public static IEnumerable<SelectListItem> GetYears(int startYear, int? endYear = null, string defaultText = null)
		{
			if (endYear == null)
				endYear = DateTime.Now.Date.Year;

			if (startYear < 0 || endYear < 0)
				throw new ArgumentOutOfRangeException(GetYears_ArgumentOutOfRange);

			if (startYear > endYear)
				throw new ArgumentOutOfRangeException(GetYears_StartYearOutOfRange);

			if (!String.IsNullOrEmpty(defaultText))
				yield return new SelectListItem()
				{
					Text = defaultText,
					Value = "",
				};

			while (startYear <= endYear)
			{
				yield return new SelectListItem()
				{
					Value = startYear.ToString(),
					Text = startYear.ToString(),
				};
				startYear++;
			}
		}
		#endregion

		#region GetYesOrNo
		/// <summary>
		/// Will get the drop down list of Yes or No.
		/// </summary>
		public static IEnumerable<SelectListItem> GetYesOrNo(string selectText = TmsConstants.DefaultSelectText, bool? selectedValue = null)
		{
			if (!String.IsNullOrEmpty(selectText))
				yield return new SelectListItem()
				{
					Text = selectText,
					Value = ""
				};

			yield return new SelectListItem()
			{
				Text = "Yes",
				Value = bool.TrueString,
				Selected = true == selectedValue,
			};

			yield return new SelectListItem()
			{
				Text = "No",
				Value = bool.FalseString,
				Selected = false == selectedValue,
			};
		}
		#endregion

		#region GetDefaultSelectListItem
		public static SelectListItem GetDefaultSelectListItem(string defaultText = null)
		{
			return new SelectListItem()
			{
				Text = defaultText ?? TmsConstants.DefaultSelectText,
				Value = string.Empty,
			};
		}
		#endregion

		#region GetMarkedSelectListItem
		public static SelectListItem GetMarkedSelectListItem(Enum value, string selectedValue)
		{
			return GetSelectListItem(value.ToDbValueString()).MarkSelected(selectedValue);
		}

		public static SelectListItem GetMarkedSelectListItem(Enum value, IEnumerable<string> selectedValues)
		{
			return GetSelectListItem(value.ToDbValueString()).MarkSelected(selectedValues);
		}

		public static SelectListItem GetMarkedSelectListItem(string value, string selectedValue, string text = null)
		{
			return GetSelectListItem(value, text).MarkSelected(selectedValue);
		}

		public static SelectListItem GetMarkedSelectListItem(string value, IEnumerable<string> selectedValues, string text = null)
		{
			return GetSelectListItem(value, text).MarkSelected(selectedValues);
		}

		public static SelectListItem GetMarkedSelectListItem(int value, string selectedValue, string text = null)
		{
			return GetSelectListItem(value, text).MarkSelected(selectedValue);
		}

		public static SelectListItem GetMarkedSelectListItem(int value, IEnumerable<string> selectedValues, string text = null)
		{
			return GetSelectListItem(value, text).MarkSelected(selectedValues);
		}
		#endregion

		#region GetSelectListItem
		public static SelectListItem GetSelectListItem(int value, string text = null)
		{
			return GetSelectListItem(value.ToString(), text);
		}

		public static SelectListItem GetSelectListItem(string value, string text = null)
		{
			return new SelectListItem
			{
				Value = value,
				Text = text ?? value,
			};
		}
		#endregion

		#region GetMarkedSelectListItems
		public static List<SelectListItem> GetMarkedSelectListItems(Type enumType, string selectedValue, bool addDefault = false)
		{
			var query = GetDbValueStringOrderedListQuery(enumType).Select(v => GetMarkedSelectListItem(v, selectedValue));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}

		public static List<SelectListItem> GetMarkedSelectListItems(Type enumType, IEnumerable<string> selectedValues, bool addDefault = false)
		{
			var query = GetDbValueStringOrderedListQuery(enumType).Select(v => GetMarkedSelectListItem(v, selectedValues));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}
		#endregion

		#region GetSelectListItems
		public static List<SelectListItem> GetSelectListItems(Type enumType, bool addDefault = false)
		{
			var query = GetDbValueStringOrderedListQuery(enumType).Select(v => GetSelectListItem(v));
			return addDefault ? query.ToDefaultPrependedList() : query.AsList();
		}
		#endregion
	}
}