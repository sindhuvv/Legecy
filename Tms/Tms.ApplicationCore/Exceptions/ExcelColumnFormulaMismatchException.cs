using System;

namespace Tms.ApplicationCore.Exceptions
{
	/// <summary>
	/// Exception thrown when trying to set a formula to a non-string property.
	/// </summary>
	public class ExcelColumnFormulaMismatchException : ApplicationException
	{
		private readonly Type _type;
		private readonly string _propertyName;

		public ExcelColumnFormulaMismatchException(Type type, string propertyName)
		{
			_type = type;
			_propertyName = propertyName;
		}

		public override string Message
		{
			get
			{
				return "[ExcelColumn] attribute on property: " + _propertyName + " for type: " + _type.Name + "  can not set Formula true because it needs to be a string.";
			}
		}
	}
}
