using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	public class ModalContext
	{
		public IHtmlContent Body { get; set; }
		public IHtmlContent Footer { get; set; }
	}

	/// <summary>
	/// A Bootstrap modal dialog
	/// </summary>
	[RestrictChildren("kpmg-modal-body", "kpmg-modal-footer")]
	public class ModalTagHelper : TagHelper
	{
		/// <summary>
		/// The title of the modal
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The Id of the modal
		/// </summary>
		public string Id { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var modalContext = new ModalContext();
			context.Items.Add(typeof(ModalTagHelper), modalContext);

			await output.GetChildContentAsync();

			var template =
$@"<div class='modal-dialog' role='document'>
    <div class='modal-content'>
      <div class='modal-header'>
        <button type = 'button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
        <h4 class='modal-title' id='{context.UniqueId}Label'>{Title}</h4>
      </div>
        <div class='modal-body'>";

			output.TagName = "div";
			output.Attributes.SetAttribute("role", "dialog");
			output.Attributes.SetAttribute("id", Id);
			output.Attributes.SetAttribute("aria-labelledby", $"{context.UniqueId}Label");
			output.Attributes.SetAttribute("tabindex", "-1");
			var classNames = "modal fade";
			if (output.Attributes.ContainsName("class"))
			{
				classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
			}
			output.Attributes.SetAttribute("class", classNames);
			output.Content.AppendHtml(template);
			if (modalContext.Body != null)
			{
				output.Content.AppendHtml(modalContext.Body);
			}
			output.Content.AppendHtml("</div>");
			if (modalContext.Footer != null)
			{
				output.Content.AppendHtml("<div class='modal-footer'>");
				output.Content.AppendHtml(modalContext.Footer);
				output.Content.AppendHtml("</div>");
			}

			output.Content.AppendHtml("</div></div>");
		}
	}

	/// <summary>
	/// The modal-body portion of a Bootstrap modal dialog
	/// </summary>
	[HtmlTargetElement("kpmg-modal-body", ParentTag = "modal")]
	public class ModalBodyTagHelper : TagHelper
	{
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var childContent = await output.GetChildContentAsync();
			var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
			modalContext.Body = childContent;
			output.SuppressOutput();
		}
	}

	/// <summary>
	/// The modal-footer portion of Bootstrap modal dialog
	/// </summary>
	[HtmlTargetElement("kpmg-modal-footer", ParentTag = "modal")]
	public class ModalFooterTagHelper : TagHelper
	{
		/// <summary>
		/// Whether or not to show a button to dismiss the dialog. 
		/// Default: <c>true</c>
		/// </summary>
		public bool ShowDismiss { get; set; } = true;

		/// <summary>
		/// The text to show on the Dismiss button
		/// Default: Cancel
		/// </summary>
		public string DismissText { get; set; } = "Cancel";


		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			if (ShowDismiss)
			{
				output.PreContent.AppendFormat(@"<button type='button' class='btn btn-default' data-dismiss='modal'>{0}</button>", DismissText);
			}
			var childContent = await output.GetChildContentAsync();
			var footerContent = new DefaultTagHelperContent();
			if (ShowDismiss)
			{
				footerContent.AppendFormat(@"<button type='button' class='btn btn-default' data-dismiss='modal'>{0}</button>", DismissText);
			}
			footerContent.AppendHtml(childContent);
			var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
			modalContext.Footer = footerContent;
			output.SuppressOutput();
		}
	}

	/// <summary>
	/// Sets the element as the item that will toggle the specified Bootstrap modal dialog
	/// </summary>
	[HtmlTargetElement(Attributes = ModalTargetAttributeName)]
	public class ModalToggleTagHelper : TagHelper
	{
		public const string ModalTargetAttributeName = "bs-toggle-modal";

		/// <summary>
		/// The id of the modal that will be toggled by this element
		/// </summary>
		[HtmlAttributeName(ModalTargetAttributeName)]
		public string ToggleModal { get; set; }


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.SetAttribute("data-toggle", "modal");
			output.Attributes.SetAttribute("data-target", $"#{ToggleModal}");
		}
	}
}
