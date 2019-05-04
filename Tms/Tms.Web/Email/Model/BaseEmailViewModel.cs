using System.Collections.Generic;

namespace Tms.Web.Email.Model
{
	public class BaseEmailViewModel
	{
		public BaseEmailViewModel()
		{
			ToList = new List<string>();
			CCList = new List<string>();
			BCCList = new List<string>();
		}
		public List<string> ToList { get; set; }

		public List<string> CCList { get; set; }

		public List<string> BCCList { get; set; }

		public string Subject { get; set; }

		public string FromAddress { get; set; }

		public virtual string EmailType
		{
			get { return this.GetType().FullName; }
		}
	}
}
