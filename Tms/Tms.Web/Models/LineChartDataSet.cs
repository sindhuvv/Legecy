using System.Collections.Generic;

namespace Tms.Web.Models
{
	public class LineChartDataSet
	{
		public string Data_Lable { get; set; }
		public List<int> Data { get; set; }
		public string BackgroundColor { get; set; }
		public string BorderColor { get; set; }
		public bool IsFill { get; set; }
	}
}
