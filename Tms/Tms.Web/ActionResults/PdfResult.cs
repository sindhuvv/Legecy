using Microsoft.AspNetCore.Http;
using Tms.ApplicationCore.Helpers;

namespace Tms.Web.ActionResults
{
	/// <summary>
	/// The Result object for serializing an PDF document to the response.
	/// </summary>
	public class PdfResult : OfficeDocumentResult
	{
		private readonly byte[] _bytes;

		/// <summary>
		/// The bytes of the pdf.
		/// </summary>
		public byte[] Bytes { get { return _bytes; } }

		public PdfResult(byte[] bytes, string filename = null)
			: base(filename)
		{
			Check.NullOrEmpty(bytes, "bytes");

			_bytes = bytes;
		}

		protected override void WriteContent(HttpResponse response)
		{
			response.Body.WriteAsync(_bytes);
		}
	}
}