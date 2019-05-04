using System.Collections.Generic;
using Tms.Web.Interfaces;

namespace Tms.Web.Helpers
{
	public class ScriptHelper : IScriptHelper
	{
		public List<string> ScriptTexts { get; } = new List<string>();
		void IScriptHelper.AddScriptText(string newScriptText)
		{
			if (!ScriptTexts.Contains(newScriptText))
			{
				ScriptTexts.Add(newScriptText);
			}
		}
	}
}
