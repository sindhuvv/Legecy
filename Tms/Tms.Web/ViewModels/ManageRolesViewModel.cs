using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tms.ApplicationCore.Models;

namespace Tms.Web.ViewModels
{
	public class ManageRolesViewModel
	{
		public ManageRolesViewModel()
		{
			Roles = new List<SecurityRoleViewModel>();
			AllRoles = new List<SecurityRoleViewModel>();
		}

		/// <summary>
		/// Gets all roles for the application.
		/// </summary>
		public List<SecurityRoleViewModel> AllRoles { get; set; }


		public List<SecurityRoleViewModel> Roles { get; set; }

		/// <summary>
		/// Will get the list of actions for the application.
		/// </summary>
		public IEnumerable<string> GetActions()
		{
			foreach (var enumValue in Enum.GetValues(typeof(Permissions)))
			{
				yield return enumValue.ToString();
			}
		}

		/// <summary>
		/// Will get the actions as a select list item including the "select action" first choice.
		/// </summary>
		public IEnumerable<SelectListItem> GetActionsAsSelectItemList()
		{
			yield return new SelectListItem()
			{
				Text = "All Actions",
				Value = "",
			};

			foreach (var action in GetActions())
			{
				yield return new SelectListItem()
				{
					Text = action,
					Value = action,
				};
			}
		}
	}

	public class SecurityRoleViewModel
	{
		public int? Id { get; set; }

		public string Practice { get; set; }

		public string RoleName { get; set; }

		public int PermFlag { get; set; }

		public List<Permissions> Actions { get; set; }

		public string Description { get; set; }

		public bool IsBuiltIn { get; set; }

		public bool IsAuthorized(string action)
		{
			var enumValue = (Permissions)Enum.Parse(typeof(Permissions), action);

			if (Actions.Contains(enumValue))
				return true;

			return false;
		}
	}

	public class SecurityUserRoleViewModel
	{
		public SecurityUserRoleViewModel()
		{
			Actions = new List<Permissions>();
			Upns = new List<int>();
		}

		public int? Id { get; set; }

		public string Practice { get; set; }

		[StringLength(1024)]
		[Required]
		public string Description { get; set; }

		[StringLength(255)]
		[Required]
		public string RoleName { get; set; }

		public bool IsBuiltIn { get; set; }

		public List<Permissions> Actions { get; set; }

		[Required(ErrorMessage = "Select atleast one Action")]
		public string CommaDelimitedSelectiedActions { get; set; }

		public List<int> Upns { get; set; }

		public int? MemberToAddUpn { get; set; }

		public IEnumerable<SelectListItem> GetActionChoices()
		{
			var actionType = typeof(Permissions);
			var list = new List<SelectListItem>();

			foreach (var value in Enum.GetValues(actionType))
			{
				var text = Enum.Parse(actionType, value.ToString()).ToString();

				///grab the description if it is on the enum.
				if (GetDescription(actionType, value) != null)
					text += " - " + GetDescription(actionType, value);

				list.Add(new SelectListItem()
				{
					Selected = Actions.Any(x => (int)x == (int)value),
					Text = text,
					Value = ((int)value).ToString(),
				});
			}

			return list;
		}

		/// <summary>
		/// Will get the description from an Attribute applied to the enum value.
		/// </summary>
		private static string GetDescription(Type actionsType, object enumValue)
		{
			var memberInfo = actionsType.GetMember(enumValue.ToString()).First();

			var description = memberInfo.GetCustomAttributes(true)
				.SingleOrDefault(x => x.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

			if (description == null)
				return null;

			return description.Description;
		}
	}
}