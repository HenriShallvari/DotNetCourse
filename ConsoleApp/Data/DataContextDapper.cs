using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ConsoleApp.Data
{
    public class DataContextDapper
    {

        private readonly string _connectionString;

        public DataContextDapper(IConfiguration config) { 

            // Controlliamo 'config' stesso
            _ = config ?? throw new ArgumentNullException(nameof(config));

            // Controlliamo il risultato di GetConnectionString e lanciamo un'eccezione se è nullo
            _connectionString = config.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' non trovata in appsettings.json");
        }


        public IEnumerable<T> LoadData<T>(string sql)
        {
            using (IDbConnection dbConn = new SqlConnection(_connectionString)){

                return dbConn.Query<T>(sql);
            }
        }

        public bool ExecuteSQL(string sql)
        {
            using (IDbConnection dbConn = new SqlConnection(_connectionString)){

                return dbConn.Execute(sql) > 0;
            }
        }

        public int ExecuteSQLWithRowCount(string sql)
        {
            using (IDbConnection dbConn = new SqlConnection(_connectionString)){

                return dbConn.Execute(sql);
            }
        }

    }
}