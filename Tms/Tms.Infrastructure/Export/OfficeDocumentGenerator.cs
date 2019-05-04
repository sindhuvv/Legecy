using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Helpers;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// Contains the interface for generating office documents.
	/// </summary>
	public class OfficeDocumentGenerator : IOfficeDocumentGenerator
	{
		internal const string NoRecordsFoundMessage = "No records found.";

		static OfficeDocumentGenerator()
		{
			new Aspose.Cells.License().SetLicense(InternalConstants.AsposeLicenseName);
		}

		Workbook IOfficeDocumentGenerator.GetWorkbook()
		{
			var workbook = new Workbook();
			while (workbook.Worksheets.Count > 1)
			{
				workbook.Worksheets.RemoveAt(workbook.Worksheets.Count - 1);
			}
			return workbook;
		}

		Workbook IOfficeDocumentGenerator.GenerateWorkbook<T>(List<T> items, string sheetName)
		{
			Check.Null(sheetName, "Missing sheetName so the sheet can be labeled.");

			var dataTable = items.ToDataTable();
			var workbook = ((IOfficeDocumentGenerator)this).GetWorkbook();
			var worksheet = workbook.Worksheets[0];
			worksheet.Name = sheetName;

			//write out headers
			var columnIndex = 0;
			foreach (var column in dataTable.Columns.Cast<System.Data.DataColumn>())
			{
				worksheet.Cells[0, columnIndex++].PutValue(column.ColumnName);
			}

			//write out data
			var rowIndex = 0;
			foreach (var row in dataTable.Rows.Cast<System.Data.DataRow>())
			{
				rowIndex++;
				columnIndex = 0;
				foreach (var column in dataTable.Columns.Cast<System.Data.DataColumn>())
				{
					worksheet.Cells[rowIndex, columnIndex++].PutValue(row[column.ColumnName].ToString());
				}
			}

			return workbook;
		}

		Workbook IOfficeDocumentGenerator.GetNoRecordsFoundWorkbook()
		{
			var workbook = ((IOfficeDocumentGenerator)this).GetWorkbook();
			var cell = workbook.Worksheets[0].Cells[0, 0];
			var style = cell.GetStyle();

			style.Font.Size = 14;
			style.Font.Color = System.Drawing.Color.Red;
			cell.SetStyle(style);
			cell.PutValue(NoRecordsFoundMessage);
			cell.Worksheet.AutoFitColumns();

			return workbook;
		}

		Workbook IOfficeDocumentGenerator.OpenWorkbook(string resourceName)
		{
			using (var stream = getStreamFromResourceName(resourceName))
			{
				var workbook = new Workbook(stream);
				workbook.Worksheets.ActiveSheetIndex = 0;

				if (workbook.Worksheets.Count > 0)
					workbook.Worksheets[0].ActiveCell = "A1";

				return workbook;
			}
		}

		private Stream getStreamFromResourceName(string resourceName)
		{
			var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);

			if (stream == null)
				stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

			if (stream == null)
				stream = GetStreamByResourceNameFromAllAssemblies(resourceName);

			return stream;
		}

		private static Stream GetStreamByResourceNameFromAllAssemblies(string resourceName)
		{
			var ass = AppDomain.CurrentDomain.GetAssemblies()
							.FirstOrDefault(x => !x.IsDynamic && x.GetManifestResourceStream(resourceName) != null);
			if (ass != null)
				return ass.GetManifestResourceStream(resourceName);

			return null;
		}
	}
}
