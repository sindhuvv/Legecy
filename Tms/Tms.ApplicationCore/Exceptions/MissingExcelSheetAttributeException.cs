using System;

namespace Tms.ApplicationCore.Exceptions
{
	/// <summary>
	/// Exception thrown when trying to populate excel based on a model that is missing the [ExcelSheet] attribute.
	/// </summary>
	public class MissingExcelSheetAttributeException : ApplicationException
	{
		private readonly Type _type;

		public MissingExcelSheetAttributeException(Type type)
		{
			_type = type;
		}

		public override string Message
		{
			get
			{
				return "Missing [ExcelSheet] attribute on the " + _type.Name + " object for excel population.";
			}
		}
	}
}
