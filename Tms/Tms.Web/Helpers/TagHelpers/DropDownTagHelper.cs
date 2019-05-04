using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("kpmg-multi-dropdown")]
	[HtmlTargetElement("kpmg-simple-dropdown")]
	public class DropDownTagHelper : TagHelper
	{
		private readonly IScriptHelper _scriptHelper;
		private readonly IHtmlGenerator _htmlGenerator;

		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext Context { get; set; }

		[HtmlAttributeName("asp-for")]
		public ModelExpression For { get; set; }

		[HtmlAttributeName("asp-items")]
		public IEnumerable<SelectListItem> Items { get; set; }

		public string Default_Select { get; set; } = "Select";

		[HtmlAttributeName("multiple")]
		public bool AllowMultiple { get; set; } = true;

		public bool Disabled { get; set; } = false;

		public DropDownTagHelper(IHtmlGenerator htmlGenerator, IScriptHelper scriptHelper)
		{
			_scriptHelper = scriptHelper;
			_htmlGenerator = htmlGenerator;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var ddType = output.TagName.Split("-")[1];
			output.TagName = string.Empty;

			if (ddType.Equals("simple") || !(For.ModelExplorer.ModelType.IsGenericType && For.ModelExplorer.ModelType.GetGenericTypeDefinition() == typeof(List<>)))
				AllowMultiple = false;

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

			var inputTag = _htmlGenerator.GenerateSelect(Context, For.ModelExplorer, null, For.Name, Items, AllowMultiple, htmlAttributes);
			output.Content.AppendHtml(inputTag);

			var script = $@"$(function () {{
                                    $('#{inputTag.Attributes["id"]}').select2({{
                                                placeholder: '{Default_Select}',
                                                theme: 'bootstrap4',
                                                width: 'resolve',
                                                minimumResultsForSearch: -1
                                            }});
                                }});
                            ";
			_scriptHelper.AddScriptText(script);
		}
	}
}