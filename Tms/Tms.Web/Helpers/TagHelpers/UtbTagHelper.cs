using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Interfaces;
using Tms.Web.Interfaces;
using static Tms.ApplicationCore.TmsEnums;

namespace Tms.Web.Helpers.TagHelpers
{
	[HtmlTargetElement("kpmg-utb")]
	public class UtbTagHelper : TagHelper
	{
		private IHtmlGenerator _htmlGenerator;
		public IScriptHelper _scriptHelper { get; }

		private IUtbService _baseService;

		public UtbTagHelper(IHtmlGenerator htmlGenerator, IScriptHelper scriptHelper,IUtbService baseService)
		{
			_htmlGenerator = htmlGenerator;
			_scriptHelper = scriptHelper;
			_baseService = baseService;
		}

		[HtmlAttributeName("asp-for")]
		public ModelExpression For { get; set; }

		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext Context { get; set; }

		[HtmlAttributeNotBound]
		public int Width { get; set; } = 300;

		[HtmlAttributeName("asp-type")]
		public UtbType Type { get; set; }

		[HtmlAttributeNotBound]
		public string Input_Value
		{
			get
			{
				if (For.ModelExplorer.Model != null)
				{
					int upn = 0;
					if (Int32.TryParse(For.ModelExplorer.Model.ToString(), out upn))
					{
						var emp = _baseService.GetEmployee(upn);
						return emp != null ? emp.HRName : string.Empty;
					}
				}
				return string.Empty;
			}
		}

		public string Default_Input { get; set; } = "Search for employees..";

		public string Default_Select { get; set; } = "Select an employee";

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var api_SearchPath = TmsUtility.GetAPIPath(Type);
			output.TagName = "";
			var host = Context.HttpContext.Request.Host;
			var mainDivTag = new TagBuilder("div");

			var inputDivTag = new TagBuilder("div");
			inputDivTag.Attributes.Add("style", $"width:{Width}px");
			inputDivTag.Attributes.Add("class", "input-group");

			var dropdownDiv = new TagBuilder("div");
			dropdownDiv.Attributes.Add("class", "drop-down-group");
			dropdownDiv.Attributes.Add("style", "display:none");

			var hiddenElement = _htmlGenerator.GenerateHidden(Context, For.ModelExplorer, For.Name, For.ModelExplorer.Model, false, "");
			var Id = hiddenElement.Attributes["id"];

			var inputSpan = new TagBuilder("span");
			inputSpan.Attributes.Add("class", "input-group-btn");
			var searchSpan = new TagBuilder("span");
			searchSpan.Attributes.Add("class", "fa fa-search");
			var clearSpan = new TagBuilder("span");
			clearSpan.Attributes.Add("class", "fa fa-eraser");
			var loadingSpan = new TagBuilder("span");
			loadingSpan.Attributes.Add("class", "fas fa-spinner fa-spin");

			var searchBtn = new TagBuilder("button");
			searchBtn.Attributes.Add("class", "btn btn-default");
			searchBtn.Attributes.Add("type", "button");
			searchBtn.Attributes.Add("id", Id + "_imgSearch");
			searchBtn.InnerHtml.AppendHtml(searchSpan);

			var clearBtn = new TagBuilder("button");
			clearBtn.Attributes.Add("class", "btn btn-default");
			clearBtn.Attributes.Add("type", "button");
			clearBtn.Attributes.Add("id", Id + "_imgClear");
			clearBtn.Attributes.Add("style", "display:none");
			clearBtn.InnerHtml.AppendHtml(clearSpan);

			var loadingBtn = new TagBuilder("button");
			loadingBtn.Attributes.Add("class", "btn btn-default");
			loadingBtn.Attributes.Add("type", "button");
			loadingBtn.Attributes.Add("id", Id + "_imgLoading");
			loadingBtn.Attributes.Add("style", "display:none");
			loadingBtn.InnerHtml.AppendHtml(loadingSpan);

			inputSpan.InnerHtml.AppendHtml(searchBtn);
			inputSpan.InnerHtml.AppendHtml(loadingBtn);
			inputSpan.InnerHtml.AppendHtml(clearBtn);

			var inputElement = new TagBuilder("input");
			inputElement.Attributes.Add("class", "form-control");
			inputElement.Attributes.Add("type", "text");
			inputElement.Attributes.Add("id", Id + "_input");
			inputElement.Attributes.Add("value", Input_Value);
			inputElement.Attributes.Add("placeholder", Default_Input);

			inputDivTag.InnerHtml.AppendHtml(inputElement);

			var selectElement = new TagBuilder("select");
			selectElement.Attributes.Add("id", Id + "_ddl");
			selectElement.Attributes.Add("class", "form-control");
			selectElement.Attributes.Add("style", $"width:{Width - 40}px");
			dropdownDiv.InnerHtml.AppendHtml(selectElement);
			inputDivTag.InnerHtml.AppendHtml(dropdownDiv);
			inputDivTag.InnerHtml.AppendHtml(inputSpan);

			mainDivTag.Attributes.Add("id", Id + "_utb");
			mainDivTag.InnerHtml.AppendHtml(inputDivTag);
			mainDivTag.InnerHtml.AppendHtml(hiddenElement);

			var scriptText = $@"
								$(function () {{
									var dd_Activated = false;
									$('#{Id}_imgSearch').click(function () {{
										var searchValue = $('#{Id}_input').val();
										if (searchValue.trim().length === 0) {{
											alert('Search parameter is empty!');
										}}
										else {{
											$('#{Id}_imgSearch').hide();
											$('#{Id}_imgLoading').show();
											$.ajax({{
												url: '{Context.HttpContext.Request.Scheme}://{Context.HttpContext.Request.Host}/{api_SearchPath}'+searchValue,
												dataType: 'json',
											}}).done(function (data) {{
												if (data.length === 0) {{
													alert('No Resuls Found');
													$('#{Id}').val('');
													$('#{Id}_utb').find('#{Id}_input').val('');
													$('#{Id}_imgSearch').show();
													$('#{Id}_imgLoading').hide();
												}}
												else if (data.length === 1) {{
													$('#{Id}_utb').find('#{Id}_input').val(data[0].text);
													$('#{Id}').val(data[0].upn);
													$('#{Id}_imgSearch').hide();
													$('#{Id}_imgLoading').hide();
													$('#{Id}_imgClear').show();
												}}
												else {{
													if (data.length > 100) {{
														alert('There are more than 100 Results.Please modify search parameter');
													}}
													var initialSelect = {{
														id: 0,
														text: 'Select',
													}}
													result = $.map(data, function (x) {{
														return {{
															id: x.value,
															text: x.text,
														}};
													}});
													result.unshift(initialSelect);
													$('#{Id}_ddl').select2({{
														placeholder: 'Select an employee',
														data: result,
													}});
													dd_Activated = true;
													$('#{Id}_utb').find('#{Id}_input').hide();
													$('#{Id}_utb').find('div.drop-down-group').show();
													$('#{Id}_imgSearch').hide();
													$('#{Id}_imgLoading').hide();
													$('#{Id}_imgClear').show();
													$('#{Id}_ddl').on('change.select2', function () {{
														var selectedText = $('#{Id}_ddl').select2('data');
														if (selectedText[0]) {{
															$('#{Id}_utb').find('#{Id}_input').val(selectedText[0].text);
															$('#{Id}').val(selectedText[0].id);
														}}
														else {{
															$('#{Id}_utb').find('#{Id}_input').val('');
															$('#{Id}').val('');
														}}
														$('#{Id}_utb').find('div.drop-down-group').hide();
														$('#{Id}_utb').find('#{Id}_input').show();
														$('#{Id}_imgSearch').hide();
														$('#{Id}_imgLoading').hide();
														$('#{Id}_imgClear').show();
													}})
												}}
											}})
												.fail(function () {{
													alert('An error occured in API request');
													$('#{Id}').val('');
													$('#{Id}_utb').find('#{Id}_input').val('');
													$('#{Id}_imgSearch').show();
													$('#{Id}_imgLoading').hide();
												}});
										}}
									}})

									$('#{Id}_imgClear').click(function () {{
										$('#{Id}').val('');
										$('#{Id}_utb').find('#{Id}_input').val('');
										if (dd_Activated) {{
											$('#{Id}_ddl').html('').select2({{ data: [{{ id: '', text: '' }}] }});
											$('#{Id}_ddl').val([]).trigger('change');
											$('#{Id}_utb').find('div.drop-down-group').hide();
										}}
										$('#{Id}_utb').find('#UPN_input').show();
										$('#{Id}_imgSearch').show();
										$('#{Id}_imgClear').hide();
									}});

									if('{Input_Value}'.length!==0){{
											$('#{Id}_imgSearch').hide();
											$('#{Id}_imgLoading').hide();
											$('#{Id}_imgClear').show();
										}}
								}})
							";
			output.Content.AppendHtml(mainDivTag);
			_scriptHelper.AddScriptText(scriptText);
		}
	}
}
