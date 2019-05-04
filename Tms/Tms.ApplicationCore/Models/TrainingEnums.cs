using System.ComponentModel;

namespace Tms.ApplicationCore.Models
{
	public enum RosterStatus
	{
		[Description("Active")]
		Active = 1,

		[Description("Transferred Out")]
		TransferredOut = 2,

		[Description("LOA")]
		LOA = 3,

		[Description("Non-Promote")]
		NonPromote = 4,

		[Description("Termed")]
		Termed = 5,
	}

	public enum TrainingPopulationKey
	{
		[Description("Audit Seniors and Associates Level 2 and 3")]
		AuditSeniorsandAssociatesLevel2and3 = 1,

		[Description("Audit Associates Level 0 and 1")]
		AuditAssociatesLevel0and1 = 2,

		[Description("Audit Seniors and Associates")]
		AuditSeniorsandAssociates = 3,

		[Description("Audit Seniors Level 2 and Greater")]
		AuditSeniorsLevel2andGreater = 4,

		[Description("Audit Seniors Level 0")]
		AuditSeniorsLevel0 = 5,

		[Description("Audit Seniors Level 1")]
		AuditSeniorsLevel1 = 6,

		[Description("Audit Associates Level 2 and Greater")]
		AuditAssociatesLevel2andGreater = 7,
	}
}