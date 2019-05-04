using System.Collections.Generic;

namespace Tms.Web.Models
{
	public class LineChart
	{
		public List<string> Labels { get; set; }
		public string Title { get; set; }
		public string X_Lable { get; set; }
		public string Y_Lable { get; set; }
		public List<LineChartDataSet> DataSet { get; set; }
	}
}
