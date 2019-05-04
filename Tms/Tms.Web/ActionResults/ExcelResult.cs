using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using System;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Helpers;
using Tms.Infrastructure.Export;

namespace Tms.Web.ActionResults
{
	/// <summary>
	/// The Result object for serializing an excel document to the response.
	/// </summary>
	public class ExcelResult : OfficeDocumentResult
	{
		private readonly Workbook _workbook;

		public ExcelResult(Workbook workbook, string filename = null)
			: base(filename)
		{
			Check.Null(workbook, "workbook");

			//check to put the date on there.
			if (!String.IsNullOrEmpty(filename) && !filename.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
				Filename = filename + DateTime.Now.ToString("_MM_dd_yyy_hh_mm_ss") + ".xlsx";
			
			_workbook = workbook;
		}

		protected override void WriteContent(HttpResponse response)
		{
			response.ContentType = TmsConstants.MimeTypes.XLSX;
			_workbook.WriteToStream(response.Body);
		}
	}
}
