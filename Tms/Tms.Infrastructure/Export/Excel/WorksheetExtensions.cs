using Aspose.Cells;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Tms.ApplicationCore.Attributes;
using Tms.ApplicationCore.Exceptions;
using Tms.ApplicationCore.Extensions;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// Contains extensions methods for working with the excel worksheet.
	/// </summary>
	public static class WorksheetExtensions
	{
		/// <summary>
		/// Will populate the worksheet with the list of items using the ExcelSheet/Column attributes.
		/// </summary>
		public static void Populate(this Worksheet sheet, IEnumerable items)
		{
			var genericTypes = items.GetType().GenericTypeArguments;
			if (genericTypes.Length != 1)
				throw new NotImplementedException("Could not determine the type of items stored in the IEnumerable.");

			var type = genericTypes.First();

			var sheetAttribute = type.GetCustomAttributesByType<ExcelSheetAttribute>(true).FirstOrDefault();

			if (sheetAttribute == null)
				throw new MissingExcelSheetAttributeException(type);

			sheet.Name = sheetAttribute.SheetName;

			if (sheetAttribute.IncludeAllProperties)
				PopulateSimple(sheet, items, type, sheetAttribute);
			else
				PopulateAttributeBased(sheet, items, type, sheetAttribute);
		}

		private static void PopulateSimple(Worksheet sheet, IEnumerable items, Type type, ExcelSheetAttribute sheetAttribute)
		{
			var propertyList = type.GetProperties().OrderBy(x => x.Name).ToList();

			if (propertyList.Any(x => x.GetCustomAttribute<ExcelColumnAttribute>() != null))
				throw new ApplicationException("ExcelColumnAttribute should not be used when IncludeAllProperties of ExcelSheetAttribute is true.");

			var currentRow = sheetAttribute.StartRow;
			var currentColumn = sheetAttribute.StartColumn;

			if (sheetAttribute.Headers)
			{
				foreach (var property in propertyList)
				{
					var cell = sheet.Cells[currentRow, currentColumn++];
					cell.PutValue(property.Name);
					SetHeaderStyle(cell);
				}

				ImplementExcelSheetOptions(sheet, sheetAttribute, currentRow, currentColumn);
				currentRow++;
				currentColumn = sheetAttribute.StartColumn;
			}

			foreach (var dataRow in items)
			{
				for (int i = 0; i < propertyList.Count; i++)
				{
					var value = propertyList[i].GetValue(dataRow);
					var cell = sheet.Cells[currentRow, currentColumn++];
					cell.PutValue(value);
				}

				currentRow++;
				currentColumn = sheetAttribute.StartColumn;
			}

			for (int i = 0; i < propertyList.Count; i++)
			{
				sheet.AutoFitColumn(currentColumn++);
			}
			currentColumn = sheetAttribute.StartColumn;
		}

		private static void PopulateAttributeBased(Worksheet sheet, IEnumerable items, Type type, ExcelSheetAttribute sheetAttribute)
		{
			var columnList = type.GetProperties()
								.Where(x => x.GetCustomAttribute<ExcelColumnAttribute>() != null)
								.Select(x => new PropertyAttributeItem(x, x.GetCustomAttribute<ExcelColumnAttribute>()))
								.OrderBy(x => x.Attribute.Order)
								.ThenBy(x => x.Property.Name)
								.ToList();

			var currentRow = sheetAttribute.StartRow;
			var currentColumn = sheetAttribute.StartColumn;

			if (sheetAttribute.Headers)
			{
				foreach (var column in columnList)
				{
					var cell = sheet.Cells[currentRow, currentColumn++];
					cell.PutValue(column.Attribute.HeaderName ?? column.Property.Name);
					SetHeaderStyle(cell);
				}

				ImplementExcelSheetOptions(sheet, sheetAttribute, currentRow, currentColumn);
				currentRow++;
				currentColumn = sheetAttribute.StartColumn;
			}

			foreach (var dataRow in items)
			{
				for (int i = 0; i < columnList.Count; i++)
				{
					var column = columnList[i];
					var value = column.Property.GetValue(dataRow);
					var cell = sheet.Cells[currentRow, currentColumn++];

					if (column.Attribute.Formula)
					{
						if (column.Property.PropertyType != typeof(string))
							throw new ExcelColumnFormulaMismatchException(type, column.Property.Name);

						cell.Formula = (string)value;
						continue;
					}

					cell.PutValue(value);
					
					//apply custom formatting.
					if (!String.IsNullOrEmpty(column.Attribute.Format))
					{
						var style = cell.GetStyle();
						style.Custom = column.Attribute.Format;
						cell.SetStyle(style);
					}
				}

				currentRow++;
				currentColumn = sheetAttribute.StartColumn;
			}

			//check if there are any auto-fit columns.  Mostly to keep the currentColumn reset code template.
			if (columnList.Any(x => x.Attribute.AutoFit))
			{
				foreach (var column in columnList)
				{
					if (column.Attribute.AutoFit)
						sheet.AutoFitColumn(currentColumn);

					currentColumn++;
				}
				currentColumn = sheetAttribute.StartColumn;
			}
		}

		private static void ImplementExcelSheetOptions(Worksheet sheet, ExcelSheetAttribute sheetAttribute, int currentRow, int currentColumn)
		{
			//freeze all the header cells if requested.
			if (sheetAttribute.FreezeHeaders)
			{
				sheet.FreezePanes(currentRow + 1, 0, 1, 0);
			}

			if (sheetAttribute.AutoFilter)
			{
				sheet.AutoFilter.SetRange(currentRow, sheetAttribute.StartColumn, currentColumn - 1);
			}
		}

		private static void SetHeaderStyle(Cell cell)
		{
			var style = cell.GetStyle();
			style.Font.IsBold = true;
			style.Font.Size = 9;
			cell.SetStyle(style);
		}

		private struct PropertyAttributeItem
		{
			public PropertyAttributeItem(PropertyInfo property, ExcelColumnAttribute attribute)
			{
				Property = property;
				Attribute = attribute;
			}

			public PropertyInfo Property;
			public ExcelColumnAttribute Attribute;
		}
	}
}
