using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tms.Web.Attributes;
using Tms.Web.Extensions;

namespace Tms.Web.Helpers
{
	[HtmlTargetElement("nav-menu")]
	public class NavMenuTagHelper : TagHelper
	{
		private IAuthorizationService _authService;
		private IHttpContextAccessor _httpContextAccessor;

		public NavMenuTagHelper(IHtmlGenerator generator, IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
		{
			_authService = authService;
			_httpContextAccessor = httpContextAccessor;
		}

		/// <summary>
		/// Name of the Controller
		/// </summary>
		[HtmlAttributeName("asp-controller")]
		public string Controller { get; set; }

		/// <summary>
		/// Name of the Controller Action Method
		/// </summary>
		[HtmlAttributeName("asp-action")]
		public string Action { get; set; }

		[HtmlAttributeName("class")]
		public string Class { get; set; }

		[HtmlAttributeName("id")]
		public string Id { get; set; }

		/// <summary>
		/// ViewContext of request
		/// </summary>
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }
		
		private MethodInfo GetActionMethod()
		{
			var controllerType = Type.GetType("Tms.Web.Controllers." + Controller + "Controller");
			if (controllerType == null)
				return null;
			var methods = controllerType.GetMethods().Where(x => x.Name == Action);

			//There can be multiple ActionMethods with same Name. We need to consider only [HttpGet] action.
			foreach (var method in methods)
			{
				if (method.CustomAttributes.Any(x => x.AttributeType == typeof(HttpGetAttribute)))
					return method;
				//if HttpGet is not written then by default .net will consider it as **[HttpGet]
				if (!method.CustomAttributes.Any(x => x.AttributeType.Name.Contains("Http", StringComparison.OrdinalIgnoreCase)))
					return method;
			}
			return null;
		}

		public async override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var actionMethod = GetActionMethod();
			if (await IsAllowed(actionMethod))
			{
				output.TagName = "li";

				var childContent = await output.GetChildContentAsync();
				string content = childContent.GetContent();

				//The action in the route will be MethodName if [Route] attribute is not specified on the ActionMethod
				var actionRoute = string.Empty;
				var routeAttribute = GetActionMethod().GetCustomAttributes(true).SingleOrDefault(x => x.GetType() == typeof(RouteAttribute));
				if (routeAttribute != null && !String.IsNullOrEmpty(routeAttribute.ToString()))
					actionRoute = (routeAttribute as RouteAttribute).Template;
				else
					actionRoute = Action;

				var sb = new StringBuilder();
				sb.Append(string.Format("<a id=\"{0}\" href=\"{1}\"><span class=\"{2}\" style=\"color:black; \"></span> &nbsp;&nbsp{3}"
				, Id, ("/" + Controller + "/" + actionRoute), ("fas " + Class), content));
				sb.Append("</a>");

				output.Content.SetHtmlContent(sb.ToString());
			}
			else
				output.SuppressOutput();
		}

		private async Task<bool> IsAllowed(MethodInfo actionMethod)
		{
			if (actionMethod != null)
			{
				var attributes = actionMethod.GetCustomAttributes(false).Where(x => x.GetType() == typeof(AuthorizeAttribute) || x.GetType() == typeof(AuthorizeIgnoreDelegatesAttribute)).ToList();
				if (attributes.Any())
				{
					foreach (var attribute in attributes)
					{
						if (attribute.GetType() == typeof(AuthorizeAttribute))
						{
							var authorizeAttribute = attribute as AuthorizeAttribute;
							var authorized = await authorizeAttribute.AssertAuthorized(_authService, _httpContextAccessor.HttpContext.User);
							if (!authorized)
								return false;
						}
						else if (attribute.GetType() == typeof(AuthorizeIgnoreDelegatesAttribute))
						{
							var authorizeAttribute = attribute as AuthorizeIgnoreDelegatesAttribute;
							var authorized = await authorizeAttribute.AssertAuthorized(_httpContextAccessor.HttpContext.User);
							if (!authorized)
								return false;
						}
					}
				}
			}
			return true;
		}
	}
}
