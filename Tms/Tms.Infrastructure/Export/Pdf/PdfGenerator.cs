using System;
using System.IO;

namespace Tms.Infrastructure.Export
{
	public class PdfGenerator : IPdfGenerator
	{
		public PdfGenerator()
		{
			new Aspose.Html.License().SetLicense(InternalConstants.AsposeLicenseName);
		}

		byte[] IPdfGenerator.ConvertHtmlToPdf(string html)
		{
			return GetPdf(html);
		}

		private static byte[] GetPdf(string html)
		{
			var stream = new MemoryStream();
			Aspose.Html.Drawing.Page page = new Aspose.Html.Drawing.Page();
			page.Size = new Aspose.Html.Drawing.Size(1200, 800);
			Aspose.Html.Rendering.Pdf.PdfRenderingOptions pdf_options = new Aspose.Html.Rendering.Pdf.PdfRenderingOptions();
			pdf_options.PageSetup.AnyPage = page;
			using (Aspose.Html.Rendering.Pdf.PdfDevice pdf_device = new Aspose.Html.Rendering.Pdf.PdfDevice(pdf_options, stream))
			using (Aspose.Html.Rendering.HtmlRenderer renderer = new Aspose.Html.Rendering.HtmlRenderer())
			using (Aspose.Html.HTMLDocument html_document = new Aspose.Html.HTMLDocument(html, ""))
			{
				renderer.Render(pdf_device, html_document);
				return stream.ToArray();
			}
		}

		//TODO: refactor
		byte[] IPdfGenerator.ConvertWebToPdf(string url)
		{
			var fullUrl = new Uri(url);
			if (!(fullUrl.Host.Equals("localhost") || fullUrl.Host.Contains("us.kworld.kpmg.com")))
				throw new ArgumentException("Invalid url.");

			var html = string.Empty;
			using (var webClient = new System.Net.WebClient())
			{
				webClient.UseDefaultCredentials = true;
				html = webClient.DownloadString(url);
			}

			//ToDo: TBD
			//var css = GetCss();
			//html = html.Replace(TmsConstants.PrintStyleCSS, "<style>" + css + @"</style>");

			return GetPdf(html);
		}
	}
}