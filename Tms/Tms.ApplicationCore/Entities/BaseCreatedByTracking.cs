using System;

namespace Tms.ApplicationCore.Entities
{
    public class BaseCreatedByTracking : ICreatedByTracking
	{
		public virtual int CreatedBy { get; private set; }
		int ICreatedByTracking.CreatedBy
		{
			get { return CreatedBy; }
			set { CreatedBy = value; }
		}

		public virtual DateTime CreatedDateTime { get; private set; }
		DateTime ICreatedByTracking.CreatedDateTime
		{
			set { CreatedDateTime = value; }
		}
	}

	public interface ICreatedByTracking
	{
		int CreatedBy { set; get; }

		DateTime CreatedDateTime { set; }
	}
}
