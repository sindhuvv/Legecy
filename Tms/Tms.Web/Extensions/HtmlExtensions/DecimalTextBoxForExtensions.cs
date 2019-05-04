using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tms.ApplicationCore.Helpers;

namespace Tms.Web.Extensions
{
	public static class DecimalTextBoxForExtensions
	{

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBox(this HtmlHelper htmlHelper, string name, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			return htmlHelper.DecimalTextBox(name, null, minValue, maxValue, decimalPlaces, includeComma);
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBox(this HtmlHelper htmlHelper, string name, object value, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			var htmlAttributes = new Dictionary<string, object>();
			return htmlHelper.DecimalTextBox(name, value, htmlAttributes,minValue, maxValue, decimalPlaces, includeComma);
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes, decimal minValue = 0, decimal maxValue = 1000000000m,int decimalPlaces = 0, bool includeComma = true)
		{
			var dictionary = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			return htmlHelper.DecimalTextBox(name, value, dictionary, minValue, maxValue, decimalPlaces, includeComma);
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBox(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			Check.False(minValue < maxValue, "Min should be less than max");
			Check.False(decimalPlaces >= 0, "negative decimal places??? Are you kidding");
			var textbox = htmlHelper.TextBox(name, value, htmlAttributes);
			return new HtmlString(textbox.ToString() + GetAutoNumeric(name, minValue, maxValue, decimalPlaces, includeComma).ToString());
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			var dictionary = new Dictionary<string, object>();

			return htmlHelper.DecimalTextBoxFor(expression, dictionary, minValue, maxValue, decimalPlaces, includeComma);
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			var dictionary = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			return htmlHelper.DecimalTextBoxFor(expression, dictionary, minValue, maxValue, decimalPlaces, includeComma);
		}

		/// <summary>
		/// Will render an auto numeric text box.
		/// </summary>
		public static HtmlString DecimalTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, decimal minValue = 0, decimal maxValue = 1000000000m, int decimalPlaces = 0, bool includeComma = true)
		{
			Check.False(minValue < maxValue, "Min should be less than max");
			Check.False(decimalPlaces >= 0, "negative decimal places??? Are you kidding");
			
			var textbox = htmlHelper.TextBoxFor(expression, htmlAttributes);
			var name = htmlHelper.IdFor(expression).ToString();
			return new HtmlString(textbox.ToString() + GetAutoNumeric(name, minValue, maxValue, decimalPlaces, includeComma).ToString());
		}

		/// <summary>
		/// Will get the chosen init script.
		/// </summary>
		private static string GetAutoNumeric(string name, decimal minValue, decimal maxValue, int decimalPlaces, bool includeComma)
		{
			var tagBuilder = new TagBuilder("div");
			tagBuilder.GenerateId(name,"");

			var commaChar = includeComma ? "," : "";
			var id = tagBuilder.Attributes["id"];
			return String.Format(@"
<script>
$(function(){{
	var txt = $('#{0}');
	var s = txt.val();
	s = s.replace(/,/g , '');
	txt.val(s);
	txt.autoNumeric('init', {{ vMin: '{1}', vMax: '{2}', mDec: '{3}', aSep: '{4}'}});
}});
</script>", id, minValue, maxValue, decimalPlaces, commaChar);
		}
	}
}
