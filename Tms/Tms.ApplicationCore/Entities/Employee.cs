using System;

namespace Tms.ApplicationCore.Entities
{
	public class Employee
	{

		/// <summary>
		/// The date used to calculate seniority considering adjustments.  aka AdjustedServiceDate.
		/// Make sure you don't mean SeniorityDate
		/// </summary>
		public DateTime AdjustedSeniorityDate { get; set; }

		/// <summary>
		/// The employee's admin assistant if they have one.
		/// </summary>
		public int? AdminAssistantUPN { get; set; }

		/// <summary>
		/// The business area key for the employee.
		/// </summary>
		public string BusinessAreaKey { get; set; }

		/// <summary>
		/// The string business unit key for the employee.
		/// </summary>
		public string BusinessUnitKey { get; set; }

		/// <summary>
		/// The city the employee is registered to.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// The cost center key for the employee.  This might be null.
		/// </summary>
		public string CostCenterKey { get; set; }

		/// <summary>
		/// The domain login for the employee.  Can be null, normally will start with "US\"
		/// </summary>
		public string DomainLogin { get; set; }

		/// <summary>
		/// The DS/MembershipServices Id for the employee.
		/// </summary>
		public string DsId { get; set; }

		/// <summary>
		/// The email address for the employee.  Will be null for R D T status.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Yet another way to store the employee's name.  This comes from exchange.
		/// </summary>
		public string EmailDisplayName { get; set; }

		/// <summary>
		/// The date the first time the employee was hired.
		/// </summary>
		public DateTime HireDate { get; set; }

		/// <summary>
		/// The name of the employee such as "Robertson,William F" as found in peoplesoft.
		/// </summary>
		public string HRName { get; set; }

		/// <summary>
		/// The last date the employee was rehired after their initial hire.
		/// </summary>
		public DateTime? LastRehireDate { get; set; }

		/// <summary>
		/// The 2 letter code describing their job.
		/// </summary>
		public string LocalJobLevel { get; set; }

		/// <summary>
		/// The string representation of the practice.
		/// </summary>
		public string PracticeKey { get; set; }

		/// <summary>
		/// Gets the preferred first name of the employee.  Can be null.
		/// </summary>
		public string PreferredFirstName { get; set; }

		/// <summary>
		/// Gets the preferred last name of the employee.  Can be null.
		/// </summary>
		public string PreferredLastName { get; set; }

		/// <summary>
		/// Gets the preferred middle name of the employee.  Can be null.
		/// </summary>
		public string PreferredMiddleName { get; set; }

		/// <summary>
		/// The primary HR segment key if applicable.  Can be null.
		/// </summary>
		public string PrimaryHRSegmentKey { get; set; }

		/// <summary>
		/// The date the employee got to their last rank.  Similiar to last promotion date.
		/// </summary>
		public DateTime RankEntryDate { get; set; }

		/// <summary>
		/// The rankId of the employee.
		/// </summary>
		public int RankId { get; set; }

		/// <summary>
		/// The SAP Id for the employee.
		/// </summary>
		public string SapId { get; set; }

		/// <summary>
		/// The Secondary HR segment key if applicable.  Can be null.
		/// </summary>
		public string SecondaryHRSegmentKey { get; set; }

		/// <summary>
		/// The date used to calculate seniority by last hire date.  aka ServiceDate.
		/// Make sure you don't mean AdjustedSeniorityDate
		/// </summary>
		public DateTime SeniorityDate { get; set; }

		/// <summary>
		/// The expected hours the employee works in a work week.
		/// </summary>
		public int StandardHours { get; set; }

		/// <summary>
		/// Gets the state or the geo location if outside united States.
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// The status of the employee.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// The length of time at current Rank.  Not all positions use this value.
		/// </summary>
		public int SubLevel { get; set; }

		/// <summary>
		/// The supervisor, if applicable, for the employee.
		/// </summary>
		public int? SupervisorUPN { get; set; }

		/// <summary>
		/// Determines whether the employee is in a permanent or temp position.
		/// </summary>
		public bool TempEmployee { get; set; }

		/// <summary>
		/// The date the employee was terminated.  Retired, Died, Quit, or was fired.
		/// </summary>
		public DateTime? TerminationDate { get; set; }

		/// <summary>
		/// The title for the employee.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The employee id for the system.
		/// </summary>
		public int UPN { get; set; }

		/// <summary>
		/// The standard pre-discounted billing rate for the employee.  Can be 0.
		/// </summary>
		public int BillRate { get; set; }

		/// <summary>
		/// Whether this employee is promoted from kpmg staff to partner level.  Can be null
		/// </summary>
		public bool? IsPromotedToPartner { get; set; }

		/// <summary>
		/// The external tracking key for PCAOB for this employee.  Can be null.
		/// </summary>
		public string PCAOBKey { get; set; }
	}
}