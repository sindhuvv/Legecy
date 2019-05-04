using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Tms.ApplicationCore.Attributes;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Helpers;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// The worksheet data binder for binding directly to excel documents in a custom syntax.
	/// </summary>
	public class WorksheetDataBinder : IWorksheetDataBinder
	{
		private const string ModelKey = "{{";

		void IWorksheetDataBinder.BindData(Worksheet sheet, object model)
		{
			var cells = sheet.Cells;

			var unknownProperties = new List<string>();
			foreach (var cellToOperateOn in GetMarkedCells(cells))
			{
				try
				{
					if (ProcessCommand(cells, cellToOperateOn))
						continue;

					var propertyName = cellToOperateOn.StringValue;
					var list = (object)null;
					var bindedPropertyName = (string)null;
					if (IsList(propertyName, model, out list, out bindedPropertyName))
					{
						ProcessList(cells, cellToOperateOn, bindedPropertyName, list);
					}
					else
					{
						SetCellValue(cellToOperateOn, propertyName, model);
					}
				}
				catch (ArgumentNullException)
				{
					unknownProperties.Add(ModelKey + cellToOperateOn.StringValue + "}}");
				}
			}

			if (unknownProperties.Any())
			{
				var sb = new StringBuilder();
				sb.AppendLine("The following properties or commands were unknown but found in the excel sheet:");
				unknownProperties.ForEach(x => sb.AppendLine(x));
				sb.AppendLine();
				throw new Exception(sb.ToString());
			}
		}

		private static void SetCellValue(Cell cellToOperateOn, string propertyName, object model)
		{
			if (propertyName.Contains("="))
			{
				var row = string.Empty;
				if (propertyName.StartsWith("="))
					row = cellToOperateOn.Row.ToString();
				else
					row = (cellToOperateOn.Row + 1).ToString();

				propertyName = propertyName.Substring(propertyName.IndexOf("="));
				cellToOperateOn.Formula = propertyName.Replace("[row]", row);
				return;
			}

			var value = GetPropertyValue(propertyName, model);
			cellToOperateOn.PutValue(value);

			var valueString = value as string;

			if (valueString == null)
				return;

			//doesn't start with url.
			if (!valueString.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !valueString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				return;

			var address = valueString;
			var displayText = valueString;

			if (propertyName.EndsWith("Url"))
			{
				try
				{
					var attempted = GetPropertyValue(propertyName.Substring(0, propertyName.Length - 3), model) as string;
					if (attempted != null)
						displayText = attempted;
				}
				catch (ArgumentNullException) { }//was going to be too much work to test if the property existed.
			}

			cellToOperateOn.Worksheet.Hyperlinks.Add(cellToOperateOn.Name, cellToOperateOn.Name, address, displayText, null);
		}

		private static bool ProcessCommand(Cells cells, Cell cellToOperateOn)
		{
			switch (cellToOperateOn.StringValue)
			{
				case "RemoveRow":
					cells.DeleteRow(cellToOperateOn.Row);
					return true;
				default:
					return false;
			}
		}

		private static void ProcessList(Cells cells, Cell cellToOperateOn, string propertyName, object listObject)
		{
			var templateRow = cellToOperateOn.Row + 1; //because it needs to insert between the user defined rows.
			var column = cellToOperateOn.Column;

			var list = (IList)listObject;
			if (list.Count == 0)
			{
				cells.DeleteRange(templateRow - 1, column, templateRow - 1, column, ShiftType.Up);
				cells.DeleteRange(templateRow - 1, column, templateRow - 1, column, ShiftType.Up);
				if (cells[templateRow - 1, column].IsErrorValue)
					cells[templateRow - 1, column].PutValue(0);
				return;
			}

			cells.InsertRange(CellArea.CreateCellArea(templateRow, column, templateRow + list.Count, column), list.Count, ShiftType.Down, true);

			//loop all the rows in the list we are binding too.
			foreach (var item in list)
			{
				SetCellValue(cells[templateRow, column], propertyName, item);
				templateRow++;
			}

			//now delete out the template cells.
			cells.DeleteRange(cellToOperateOn.Row, column, cellToOperateOn.Row, column, ShiftType.Up);
			cells.DeleteRange(templateRow - 1, column, templateRow - 1, column, ShiftType.Up);
		}

		//determines if any part of the propertyName is a List<T> and will return that list as well as the 
		//list binding property.
		private static bool IsList(string propertyName, object model, out object listModel, out string bindedPropertyName)
		{
			//splits original into first part, then everything else.
			//List.Name.Length -> List and Name.Length
			bindedPropertyName = propertyName;
			while (bindedPropertyName.Contains("."))
			{
				var index = bindedPropertyName.IndexOf(".");
				propertyName = bindedPropertyName.Substring(0, index);
				bindedPropertyName = bindedPropertyName.Substring(index + 1);

				model = GetPropertyValue(propertyName, model);
				var modelType = model.GetType();

				//check for a List<T> property
				if (modelType.GenericTypeArguments.Length == 1)
				{
					listModel = model;
					var genericListType = typeof(IEnumerable<>);
					var listType = genericListType.MakeGenericType(modelType.GenericTypeArguments);
					if ((modelType == listType) || (modelType.IsSubclassOf(listType) || listType.IsAssignableFrom(modelType)))
						return true;
				}
			}

			listModel = null;
			bindedPropertyName = null;
			return false;
		}

		/// <summary>
		/// Gets the actual value to be placed into excel.
		/// </summary>
		private static object GetPropertyValue(string propertyName, object model)
		{
			var originalPropertyName = propertyName;
			var value = model;
			var propertyInfo = (PropertyInfo)null;
			var modelType = value.GetType();

			//walk the . notation looking for the correct value to return.
			while (propertyName.Contains("."))
			{
				var workingPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
				modelType = value.GetType();

				Check.Null(modelType, "modelType", "NULL check on " + propertyName + " has failed.  Need additional messaging.");

				propertyInfo = modelType.GetProperty(workingPropertyName);

				Check.Null(propertyInfo, "propertyName", "Could not find propertyName: " + workingPropertyName + " on model type: " + modelType.Name);

				value = propertyInfo.GetValue(value);
				propertyName = propertyName.Replace(workingPropertyName + ".", "");
			}

			if (value == null)
				throw new NullReferenceException(originalPropertyName + " has a null somewhere in the change and can't be determined.");

			modelType = value.GetType();
			propertyInfo = modelType.GetProperty(propertyName);
			Check.Null(propertyInfo, "propertyName", "Could not find propertyName: " + propertyName + " on model type: " + modelType.Name);
			value = propertyInfo.GetValue(value);

			//if the value comes back and null, then check to see if there is an override to ensure the proper string values gets written
			//out.  This supports the "N/A" case or anything else.  Maybe in the future conditional formatting or something.
			if (value == null)
			{
				var attribute = propertyInfo.GetCustomAttribute<ExcelNullAttribute>();
				if (attribute != null)
					value = attribute.NullValue;
			}

			if (propertyInfo.PropertyType.IsEnum)
				return ((Enum)value).ToDescriptionOrDbValue();

			//This covers the Nullable<Enum> case.
			if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericArguments().Any() && propertyInfo.PropertyType.GetGenericArguments()[0].IsEnum)
				return ((Enum)value).ToDescriptionOrDbValue();

			if (propertyInfo.PropertyType == typeof(Boolean))
				return ((Boolean)value).ToYesNoString();

			if (propertyInfo.PropertyType == typeof(Boolean?))
				return ((Boolean?)value).ToYesNoString();

			if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericArguments().Any() && propertyInfo.PropertyType.GetGenericArguments()[0] == typeof(Boolean))
				return ((Boolean)value).ToYesNoString();

			if (propertyInfo.PropertyType == typeof(DateTime))
				return ((DateTime)value).ToKpmgShortDateString();

			if (propertyInfo.PropertyType == typeof(DateTime?))
				return ((DateTime?)value).ToKpmgShortDateString();

			if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericArguments().Any() && propertyInfo.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
				return ((DateTime)value).ToKpmgShortDateString();

			return value;
		}

		/// <summary>
		/// Will get all command cells by looking at the {{}} syntax
		/// </summary>
		private static IEnumerable<Cell> GetMarkedCells(Cells cells)
		{
			var options = new FindOptions()
			{
				SearchNext = true,
				LookAtType = LookAtType.StartWith,
				LookInType = LookInType.Values,
			};

			while (true)
			{
				var cell = (Cell)null;
				cell = cells.Find(ModelKey, cell, options);

				if (cell == null)
					yield break;

				var testValue = cell.StringValue;

				if (testValue == null || !testValue.EndsWith("}}"))
					throw new Exception("Unclosed bracket on " + testValue);

				cell.PutValue(testValue.Substring(ModelKey.Length, testValue.Length - ModelKey.Length - 2));
				yield return cell;
			}
		}
	}
}
