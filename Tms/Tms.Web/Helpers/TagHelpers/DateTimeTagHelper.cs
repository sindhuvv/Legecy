using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("kpmg-date")]
	public class DateTagHelper : TagHelper
	{
		private IHtmlGenerator _htmlGenerator;

		private IScriptHelper _scriptHelper;

		public DateTagHelper(IHtmlGenerator htmlGenerator, IScriptHelper scriptHelper)
		{
			_htmlGenerator = htmlGenerator;
			_scriptHelper = scriptHelper;
		}

		/// <summary>
		/// View Model Property to bind as Date Control
		/// </summary>
		[HtmlAttributeName("asp-for")]
		public ModelExpression For { get; set; }

		/// <summary>
		/// ViewContext of request
		/// </summary>
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		/// <summary>
		/// Date format default set to 'mm/dd/yy'
		/// </summary>
		[HtmlAttributeName("date-format")]
		public string Format { get; set; } = "mm/dd/yy";


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagMode = TagMode.SelfClosing;
			output.TagName = "";

			var htmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
			{
				{ "name", For.Name },
				{"class","form-control"}
			};

			for (int i = 0; i < output.Attributes.Count; i++)
			{
				var item = output.Attributes[i];
				if (!htmlAttributes.ContainsKey(item.Name))
					htmlAttributes.Add(item.Name, item.Value);
			}

			var inputTag = _htmlGenerator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.ModelExplorer.Model, null, htmlAttributes);

			output.Content.AppendHtml(inputTag);

			var script = $@" $(function () {{
                                    $('#{inputTag.Attributes["id"]}').datepicker({{dateFormat:'{Format}'}});
                                    $('#{inputTag.Attributes["id"]}').datepicker('setDate','{For.ModelExplorer.Model}');
                                }});
                            ";

			_scriptHelper.AddScriptText(script);
		}
	}
}
