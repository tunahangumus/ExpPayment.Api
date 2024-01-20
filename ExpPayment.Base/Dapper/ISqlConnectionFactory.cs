
using System.Data;

namespace ExpPayment.Base.Dapper;

public interface ISqlConnectionFactory
{
	IDbConnection Create();
}
