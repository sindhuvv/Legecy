
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Tms.Web.Models;

namespace Tms.Web.ViewModels
{
	public class SampleViewModel
	{
		public LineChart LineChart { get; set; }

		public PieDoughnutChart PieChart { get; set; }

		public PieDoughnutChart DoughnutChart { get; set; }

		public DateTime? DateSample { get; set; }

		public int? UPN { get; set; } = 2798299;

		public int SimpleUPN { get; set; }

        public List<int> MultiUPN { get; set; }

		public string Notes { get; set; }

		public static SelectList GetSelectList()
        {
            List<object> entries = new List<object>();
            for (int i = 1; i < 12; i++)
            {
                entries.Add(new
                {
                    UPN = i,
                    Name = i
                });
            }
            return new SelectList(entries, "UPN", "Name");
        }
    }
}
