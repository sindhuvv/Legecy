using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement(Attributes = DeleteRecordAttributeName)]
	public class DeleteRecordTagHelper : TagHelper
	{
		private const string DeleteRecordAttributeName = "delete-record";
		private IScriptHelper _scriptHelper { get; }
		private IHttpContextAccessor _httpContextAccessor;

		[HtmlAttributeName(DeleteRecordAttributeName)]
		public bool DeleteRecord { get; set; } = false;
		
		public DeleteRecordTagHelper(IScriptHelper scriptHelper, IHttpContextAccessor httpContextAccessor)
		{
			_scriptHelper = scriptHelper;
			_httpContextAccessor = httpContextAccessor;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (DeleteRecord)
			{				
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
					id = tblName + tblId + "-ctrl";
					output.Attributes.Add("id", id);
				}

				var apiPath = "api/Training/DeleteRecord/";
				var scriptText = $@"
				$('#{id}').on('click', function () {{
					$.ajax({{
						url: '{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{apiPath}'+'{tblName}/'+'{tblId}',
						dataType: 'json',
						success: function (){{
							window.location.reload();
						}}
					}});
				}});
				";
				_scriptHelper.AddScriptText(scriptText);
			}
		}
	}
}
