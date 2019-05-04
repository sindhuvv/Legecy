using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Tms.Web.Interfaces;
using Tms.Web.Models;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("kpmg-pie-chart")]
	[HtmlTargetElement("kpmg-doughnut-chart")]
	public class PieDoughnutChartTagHelper : TagHelper
	{
		private IScriptHelper _scriptHelper;

		public PieDoughnutChartTagHelper(IScriptHelper scriptHelper)
		{
			_scriptHelper = scriptHelper;
		}

		[HtmlAttributeName("data")]
		public PieDoughnutChart ChartData { get; set; }

		[HtmlAttributeName("id")]
		public string Id { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var chartType = output.TagName.Split("-")[1];

			output.TagName = "canvas";
			output.Attributes.Add(new TagHelperAttribute("id", Id));

			var script = $@"$(function () {{

                                 var config{Id} = {{
                                                type: '{chartType}',
                                                data: {{
                                                    datasets: [{this.ConstructDataSet(ChartData.DataSet)}],
                                                    labels: ['{string.Join("','", ChartData.Labels)}']
                                                            }},
                                                options: {{
                                                    responsive: true,
                                                    legend: {{
                                                        position: 'top',
                                                    }},
                                                    title: {{
                                                        display: true,
                                                        text: '{ChartData.Title}'
                                                    }},
                                                    animation: {{
                                                        animateScale: true,
                                                        animateRotate: true
                                                    }}
                                                  }}
                                                }};

                                this.{Id} = new Chart(document.getElementById('{Id}').getContext('2d'), config{Id});
                    }})"
				  ;

			_scriptHelper.AddScriptText(script);
		}

		private string ConstructDataSet(List<PieDoughnutChartDataSet> dataSetItems)
		{
			var dataSet = new List<String>();
			foreach (var item in dataSetItems)
			{
				dataSet.Add($@"{{ 
                                label: '{item.Data_Lable}',
                                backgroundColor: ['{string.Join("','", item.BackgroundColor)}'],
                                borderColor: ['{string.Join("','", item.BorderColor)}'],
                                data: [{string.Join(",", item.Data)}],                                     
                              }}"
							);
			}

			return String.Join(",", dataSet);
		}
	}
}
