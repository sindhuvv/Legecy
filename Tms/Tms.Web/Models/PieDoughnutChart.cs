using System.Collections.Generic;

namespace Tms.Web.Models
{
	public class PieDoughnutChart
	{
		public List<string> Labels { get; set; }
		public string Title { get; set; }
		public List<PieDoughnutChartDataSet> DataSet { get; set; }
	}
}
