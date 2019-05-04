using System;
using System.Drawing;
using static Tms.ApplicationCore.TmsEnums;

namespace Tms.ApplicationCore
{
	public static class TmsUtility
	{
		public static string GetRGBStringValue(Color color)
		{
			return "rgb(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + ")";
		}

		public static string GetAPIPath(UtbType type)
		{
			switch (type)
			{
				case UtbType.Employee:
					return "api/Utb/SearchEmployee/";
				default:
					throw new ArgumentOutOfRangeException("Unknown U Type: " + type);
			}
		}
	}
}
