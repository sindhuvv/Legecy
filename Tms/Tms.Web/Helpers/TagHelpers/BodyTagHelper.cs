using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("body")]
	public class BodyTagHelper : TagHelper
	{
		private IScriptHelper _scriptHelper;

		public BodyTagHelper(IScriptHelper scriptHelper)
		{
			_scriptHelper = scriptHelper;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			(await output.GetChildContentAsync()).GetContent();

			var sb = new StringBuilder();
			sb.AppendLine("<script>");
			_scriptHelper.ScriptTexts.ForEach(x => sb.AppendLine(x));
			sb.AppendLine("</script>");

			output.PostContent.AppendHtml(sb.ToString());
		}
	}
}
