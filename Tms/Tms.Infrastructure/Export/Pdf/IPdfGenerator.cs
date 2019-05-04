namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// Interface for working with pdf files.
	/// </summary>
	public interface IPdfGenerator
	{
		/// <summary>
		/// Will convert the html passed into a byte array that can be saved as a PDF file to returned as a web response.
		/// </summary>
		byte[] ConvertHtmlToPdf(string html);
		/// <summary>
		/// Will convert the html passed from web url into a byte array that can be saved as a PDF file to returned as a web response.
		/// </summary>
		byte[] ConvertWebToPdf(string url);
	}
}