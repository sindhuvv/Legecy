using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tms.Web.ViewModels
{
	public class ImpersonateViewModel
	{
		[DisplayName("User")]
		[Required]
		public int? Upn { get; set; }
	}
}