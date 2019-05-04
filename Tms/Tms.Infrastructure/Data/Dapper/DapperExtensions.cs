using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Tms.Infrastructure.Data
{
	public static class DapperExtensions
	{
		/// <summary>
		/// Returns a Table Value Parameter for Dapper of type dbo.IntValueTable
		/// </summary>
		/// <param name="values">The values to be passed as a table.</param>
		/// <returns>SqlMapper.ICustomQueryParameter.</returns>
		/// <remarks>The table type dbo.IntValueTable must be defined in the current database. </remarks>
		public static SqlMapper.ICustomQueryParameter AsTableValuedParameter(this IEnumerable<int> values)
		{
			var valueTable = new DataTable();
			valueTable.Columns.Add("Value", typeof(int));
			foreach (var item in values)
			{
				var row = valueTable.NewRow();
				row[0] = item;
				valueTable.Rows.Add(row);
			}
			return valueTable.AsTableValuedParameter("dbo.IntValueTable");
		}

		/// <summary>
		/// Returns a Table Value Parameter for Dapper of type dbo.StringValueTable
		/// </summary>
		/// <param name="values"> The values to be passed as a table.</param>
		/// <returns>SqlMapper.ICustomQueryParameter.</returns>
		/// <remarks>The table type dbo.StringValueTable must be defined in the current database. </remarks>
		public static SqlMapper.ICustomQueryParameter AsTableValuedParameter(this IEnumerable<string> values)
		{
			var valueTable = new DataTable();
			valueTable.Columns.Add("Value", typeof(string));
			foreach (var item in values)
			{
				var row = valueTable.NewRow();
				row[0] = item;
				valueTable.Rows.Add(row);
			}
			return valueTable.AsTableValuedParameter("dbo.StringValueTable");
		}
	}
}
