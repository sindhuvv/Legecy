using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement(Attributes = AutoSaveAttributeName)]
	public class AutoSaveTagHelper : TagHelper
	{
		private const string AutoSaveAttributeName = "auto-save";
		private IScriptHelper _scriptHelper { get; }
		private IHttpContextAccessor _httpContextAccessor;

		[HtmlAttributeName(AutoSaveAttributeName)]
		public bool AutoSave { get; set; } = false;
		
		public AutoSaveTagHelper(IScriptHelper scriptHelper, IHttpContextAccessor httpContextAccessor)
		{
			_scriptHelper = scriptHelper;
			_httpContextAccessor = httpContextAccessor;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (AutoSave)
			{
				var colName = string.Empty;
				var colNameAttribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals(ColumnNameTagHelper.ColumnNameAttributeName));
				if (colNameAttribute != null)
					colName = colNameAttribute.Value.ToString();
				if (String.IsNullOrEmpty(colName))
				{
					var forAttribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals("asp-for"));
					colName = ((ModelExpression)forAttribute.Value).Name;
				}

				var tblName = string.Empty;
				var tblNameAttribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals(TableNameTagHelper.TableNameAttributeName));
				if (tblNameAttribute != null)
					tblName = tblNameAttribute.Value.ToString();

				var tblId = string.Empty;
				var tblIdAttribute = context.AllAttributes.SingleOrDefault(x => x.Name.Equals(TableIdentifierTagHelper.TableIdentifierAttributeName));
				if (tblIdAttribute != null)
					tblId = tblIdAttribute.Value.ToString();

				var id = string.Empty;
				var idAttribute = output.Attributes.SingleOrDefault(x => x.Name.Equals("id"));
				if (idAttribute != null)
				{
					id = idAttribute.Value.ToString();
				}
				else
				{
					id = tblName + colName + tblId + "-ctrl";
					output.Attributes.Add("id", id);
				}

				var apiPath = "api/Training/SaveData/";
				var scriptText = $@"
				$('#{id}').on('change', function () {{
					$.ajax({{
						url: '{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{apiPath}'+'{tblName}/'+'{colName}/'+$(this).val()+'/{tblId}',
						dataType: 'json',
					}})
				}});
				";
				_scriptHelper.AddScriptText(scriptText);
			}
		}
	}

	[HtmlTargetElement(Attributes = ColumnNameAttributeName)]
	public class ColumnNameTagHelper : TagHelper
	{
		public const string ColumnNameAttributeName = "column-name";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(ColumnNameAttributeName)]
		public string ColumnName { get; set; }
	}

	[HtmlTargetElement(Attributes = TableNameAttributeName)]
	public class TableNameTagHelper : TagHelper
	{
		public const string TableNameAttributeName = "table-name";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(TableNameAttributeName)]
		public string TableName { get; set; }
	}

	[HtmlTargetElement(Attributes = TableIdentifierAttributeName)]
	public class TableIdentifierTagHelper : TagHelper
	{
		public const string TableIdentifierAttributeName = "table-identifier";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(TableIdentifierAttributeName)]
		public string TableIdentifier { get; set; }
	}
}
