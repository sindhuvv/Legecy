using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore;

namespace Tms.Infrastructure.Data
{
	public class TmsDapper : ITmsDapper
	{
		private IConfiguration _configuration;
		private readonly string connectionString;

		public TmsDapper(IConfiguration configuration)
		{
			_configuration = configuration;
			connectionString = _configuration.GetConnectionString(TmsConstants.ConnectionStringName);
		}
		protected virtual string GetConnectionString()
		{
			if (string.IsNullOrEmpty(connectionString))
				throw new ApplicationException("Could not find a connection string");

			return connectionString;
		}

		async Task<IEnumerable<T>> ITmsDapper.Query<T>(string commandText, object param, int? commandTimeout)
		{
			using (var connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();

				var item = await SqlMapper.QueryAsync<T>(connection
					, commandText
					, param: param
					, commandTimeout: commandTimeout ?? 30
				);
				var list = item.ToList();

				return list;
			}
		}

		async Task<int> ITmsDapper.QueryNonQuery(string commandText, object param, int? commandTimeout)
		{
			using (var connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();

				var result = await SqlMapper.ExecuteAsync(connection
					, commandText
					, param: param
					, commandTimeout: commandTimeout ?? 30
				);

				return result;
			}
		}

		async Task<T> ITmsDapper.QueryComplex<T>(Func<SqlMapper.GridReader, T> func, string commandText, object param, int? commandTimeout)
		{
			using (var connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				var reader = await SqlMapper.QueryMultipleAsync(connection
					, commandText
					, param: param
					, commandTimeout: commandTimeout ?? 30
				);

				var returnValue = func(reader);

				return returnValue;
			}
		}

		async Task<int> ITmsDapper.QueryAsStoredProcedureNonQuery(string commandText, object param, int? commandTimeout)
		{
			using (var connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();

				var result = await SqlMapper.ExecuteAsync(connection
					, commandText
					, param: param
					, commandTimeout: commandTimeout ?? 30
					, commandType: CommandType.StoredProcedure
				);

				return result;
			}
		}
	}
	public interface ITmsDapper
	{
		Task<IEnumerable<T>> Query<T>(string commandText, object param = null, int? commandTimeout = 30);

		Task<int> QueryNonQuery(string commandText, object param = null, int? commandTimeout = 30);

		Task<T> QueryComplex<T>(Func<SqlMapper.GridReader, T> func, string commandText, object param = null, int? commandTimeout = 30);

		Task<int> QueryAsStoredProcedureNonQuery(string commandText, object param = null, int? commandTimeout = 30);
	}
}
