using Aspose.Cells;
using System.Drawing;

namespace Tms.Web.Extensions
{
	public static class WorksheetExtensions
	{
		public static void SetHeaderStyle(this Worksheet worksheet, string cellNumber, string rangeCellNumber = null)
		{
			var style = worksheet.Cells[cellNumber].GetStyle();
			style.HorizontalAlignment = TextAlignmentType.Center;
			style.ForegroundColor = Color.LightGray;
			style.Pattern = BackgroundType.Solid;
			style.Font.IsBold = true;
			style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Gray);
			style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Gray);

			if (rangeCellNumber != null)
			{
				var range = worksheet.Cells.CreateRange(cellNumber, rangeCellNumber);
				range.SetStyle(style);
			}
			else
				worksheet.Cells[cellNumber].SetStyle(style);
		}
	}
}