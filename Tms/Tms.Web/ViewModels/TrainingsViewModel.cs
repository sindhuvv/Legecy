using Tms.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Tms.Web.ViewModels
{
	public class TrainingsViewModel
	{
		public IReadOnlyList<Training> Trainings { get; set; }
		public IReadOnlyList<TrainingSession> Sessions { get; set; }
	}
}
