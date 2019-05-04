using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Tms.Web.Interfaces;
using Tms.Web.Models;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("kpmg-line-chart")]
	public class LineChartTagHelper : TagHelper
	{
		private IScriptHelper _scriptHelper;

		public LineChartTagHelper(IScriptHelper scriptHelper)
		{
			_scriptHelper = scriptHelper;
		}

		[HtmlAttributeName("data")]
		public LineChart ChartData { get; set; }

		[HtmlAttributeName("id")]
		public string Id { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "canvas";
			output.Attributes.Add(new TagHelperAttribute("id", Id));

			var script = $@"$(function () {{

                         var lineChartData{Id} =
                                            {{
                                                labels: ['{String.Join("','", ChartData.Labels)}'],
                                                datasets:[{this.ConstructDataSet(ChartData.DataSet)}]
                                            }}


                            var options{Id} = {{
                                            responsive: true,
                                            title: {{
                                                display: true,
                                                text: '{ChartData.Title}'
                                            }},
                                            tooltips: {{
                                                mode: 'index',
                                                intersect: false,
                                                }},
                                            hover: {{
                                                mode: 'nearest',
                                                intersect: true
                                            }},
                                            scales: {{
                                                xAxes: [{{
                                                    display: true,
                                                    scaleLabel: {{
                                                        display: true,
                                                        labelString: '{ChartData.X_Lable}'
                                                    }}
                                                }}],
                                                yAxes: [{{
                                                    display: true,
                                                    scaleLabel: {{
                                                        display: true,
                                                        labelString: '{ChartData.Y_Lable}'
                                                    }}
                                                }}]
                                              }}
                                            }}

                            var config{Id} = {{
                                            type: 'line',
                                            data: lineChartData{Id},
                                            options: options{Id}
                                         }};

                         this.{Id} = new Chart(document.getElementById('{Id}').getContext('2d'), config{Id});
                    }})"
				  ;

			_scriptHelper.AddScriptText(script);
		}

		private string ConstructDataSet(List<LineChartDataSet> dataSetItems)
		{
			var dataSet = new List<String>();
			foreach (var item in dataSetItems)
			{
				dataSet.Add($@"{{ 
                                label: '{item.Data_Lable}',
                                fill: {item.IsFill.ToString().ToLower()},
                                backgroundColor: '{item.BackgroundColor}',
                                borderColor: '{item.BorderColor}',
                                data: [{string.Join(",", item.Data)}],                                     
                              }}"
							);
			}

			return string.Join(",", dataSet);
		}
	}
}
