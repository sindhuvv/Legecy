using System.Collections.Generic;

namespace Tms.Web.Models
{
	public class PieDoughnutChartDataSet
	{
		public string Data_Lable { get; set; }
		public List<int> Data { get; set; }
		public List<string> BackgroundColor { get; set; }
		public List<string> BorderColor { get; set; }
	}
}
