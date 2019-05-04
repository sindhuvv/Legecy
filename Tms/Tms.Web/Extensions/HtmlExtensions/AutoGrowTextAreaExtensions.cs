using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq.Expressions;

namespace Tms.Web.Extensions
{
	public static class AutoGrowTextAreaExtensions
	{
		private const string Script = @"<script>$(function(){{tms.makeAutoGrowTextArea($('#{0}'));}});</script>";

		public static HtmlString AutoGrowTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
		{
			var element = htmlHelper.TextAreaFor(expression, htmlAttributes);
			return AutoGrowTextAreaFor(htmlHelper, expression, element);
		}

		public static HtmlString AutoGrowTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes = null)
		{
			var element = htmlHelper.TextAreaFor(expression, rows, columns, htmlAttributes);
			return AutoGrowTextAreaFor(htmlHelper, expression, element);
		}

		private static HtmlString AutoGrowTextAreaFor<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, HtmlString element)
		{
			var id = htmlHelper.IdFor(expression).ToString();
			return new HtmlString(element + Environment.NewLine + GetInitScript(id));
		}

		public static HtmlString AutoGrowTextArea(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes = null)
		{
			var element = htmlHelper.TextArea(name, value, htmlAttributes);
			return AutoGrowTextArea(name, element);
		}

		public static HtmlString AutoGrowTextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, object htmlAttributes = null)
		{
			var element = htmlHelper.TextArea(name, value, rows, columns, htmlAttributes);
			return AutoGrowTextArea(name, element);
		}

		private static HtmlString AutoGrowTextArea(string name, IHtmlContent element)
		{
			return new HtmlString(element + Environment.NewLine + GetInitScript(name));
		}

		private static string GetInitScript(string id)
		{
			return String.Format(Script, id);
		}
	}
}