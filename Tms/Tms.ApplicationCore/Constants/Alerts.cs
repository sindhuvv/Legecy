namespace Tms.ApplicationCore
{
	public static partial class TmsConstants
	{
		/// <summary>
		/// Contains common notification messages.
		/// </summary>
		public static class Alerts
		{
			public const string ChangesSaved = "Changes saved successfully.";
			public const string FileUploaded = "File uploaded successfully.";
			public const string InvalidFile = "Please select a valid file.";
			public const string Saving = "Saving...";
			public const string Adding = "Adding...";
			public const string AddSuccess = "Record added successfully.";
			public const string AddFail_Duplicate = "Add failed. A record already exists with the same Id or Name.";
			public const string UpdateFail_Duplicate = "Update failed. A record already exists with the same Name.";
		}
	}
}
