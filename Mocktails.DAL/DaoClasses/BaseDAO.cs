using System.Data;
using Microsoft.Data.SqlClient;

namespace Mocktails.DAL.DaoClasses;

public abstract class BaseDAO
{
    private string _connectionString;

    protected BaseDAO(string connectionString) => _connectionString = connectionString;

    protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
