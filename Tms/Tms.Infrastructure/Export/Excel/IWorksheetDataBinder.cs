using Aspose.Cells;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// interface for binding a data model to an excel worksheet.
	/// </summary>
	public interface IWorksheetDataBinder
	{
		/// <summary>
		/// Will walk the sheet looking for all {{Model.XXX}} properties to bind to.
		/// </summary>
		void BindData(Worksheet sheet, object model);
	}
}
