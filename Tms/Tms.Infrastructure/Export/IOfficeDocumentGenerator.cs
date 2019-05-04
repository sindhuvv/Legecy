using Aspose.Cells;
using System.Collections.Generic;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// Contains the interface for generating office documents.
	/// </summary>
	public interface IOfficeDocumentGenerator
	{
		/// <summary>
		/// Will get an empty workbook.
		/// </summary>
		Workbook GetWorkbook();

		/// <summary>
		/// Will generate a raw file based on the list passed in.
		/// It will call .ToString() on each data type to remove any possibly of data conversion issues.  
		/// </summary>
		Workbook GenerateWorkbook<T>(List<T> items, string sheetName);

		/// <summary>
		/// Will get a workbook with 'No records found' sheet.
		/// </summary>
		Workbook GetNoRecordsFoundWorkbook();

		/// <summary>
		/// Will open an existing excel file to Workbook object.
		/// </summary>
		Workbook OpenWorkbook(string resourceName);
	}
}
