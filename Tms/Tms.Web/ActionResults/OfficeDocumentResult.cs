using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tms.Web.ActionResults
{
	/// <summary>
	/// Base class for sending office documents to the browser.
	/// </summary>
	public abstract class OfficeDocumentResult : ActionResult
	{
		public OfficeDocumentResult(string filename = null)
		{
			Filename = filename;
		}

		public override void ExecuteResult(ActionContext context)
		{
			var response = context.HttpContext.Response;

			if (!String.IsNullOrEmpty(Filename))
				response.Headers.Add("Content-Disposition", "attachment;filename = " + Filename);
			else
				response.Headers.Add("Content-Disposition", "attachment;");

			WriteContent(context.HttpContext.Response);
		}

		/// <summary>
		/// Method required to actually send the content to the response object.
		/// </summary>
		protected abstract void WriteContent(HttpResponse response);

		/// <summary>
		/// The filename to be sent to the browser.
		/// </summary>
		public string Filename { get; protected set; }
	}
}
