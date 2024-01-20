using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Base.Dapper;

public class SqlConnectionFactory : ISqlConnectionFactory
{
	private readonly string _connectionString;

	public SqlConnectionFactory(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("PostgresSqlConnection") ??
			throw new ApplicationException("Connection string is missing.");
	}

	public IDbConnection Create()
	{
		return new NpgsqlConnection(_connectionString);
	}
}
