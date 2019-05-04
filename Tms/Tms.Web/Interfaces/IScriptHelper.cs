using System.Collections.Generic;

namespace Tms.Web.Interfaces
{
	public interface IScriptHelper
	{
		List<string> ScriptTexts { get; }
		void AddScriptText(string newScriptText);
	}
}
