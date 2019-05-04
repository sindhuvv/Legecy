using System;

namespace Tms.ApplicationCore.Entities
{
    public class BaseLastUpdatedByTracking : ILastUpdatedByTracking
	{
		public virtual int LastUpdatedBy { get; private set; }
		int ILastUpdatedByTracking.LastUpdatedBy
		{
			set { LastUpdatedBy = value; }
		}

		public virtual DateTime LastUpdatedDateTime { get; private set; }
		DateTime ILastUpdatedByTracking.LastUpdatedDateTime
		{
			set { LastUpdatedDateTime = value; }
		}
	}

	public interface ILastUpdatedByTracking
	{
		int LastUpdatedBy { set; }

		DateTime LastUpdatedDateTime { set; }
	}
}
