using System;
using Tms.Infrastructure.Logging;

namespace Tms.Web.ViewModels
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		public LogDetails Details { get; set; }
	}
}