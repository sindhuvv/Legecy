using System;
using System.ComponentModel;

namespace Tms.ApplicationCore.Models
{
	[Flags]
	public enum Permissions
	{
		[Description("None")]
		None = 1,

		[Description("View Training")]
		ViewTraining = 2,

		[Description("Add Training")]
		AddTraining = 4,

		[Description("Update Training")]
		UpdateTraining = 8,

		[Description("Delete Training")]
		DeleteTraining = 16,

		[Description("View Session")]
		ViewSession = 32,

		[Description("Add Session")]
		AddSession = 64,

		[Description("Update Session")]
		UpdateSession = 128,

		[Description("Delete Session")]
		DeleteSession = 256,

		[Description("View Roster")]
		ViewRoster = 512,

		[Description("Add Roster")]
		AddRoster = 1024,

		[Description("Update Roster")]
		UpdateRoster = 2048,

		[Description("Delete Roster")]
		DeleteRoster = 4096
	}
}
