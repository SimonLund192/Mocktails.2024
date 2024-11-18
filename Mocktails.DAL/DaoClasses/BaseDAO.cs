using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Mocktails.DAL.DaoClasses;
public abstract class BaseDAO
{
    private string _connectionString;

    protected BaseDAO(string connectionString) => _connectionString = connectionString;

    protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
