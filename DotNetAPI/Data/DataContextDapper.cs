using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNetAPI.Data;

public class DataContextDapper(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public IEnumerable<T> LoadData<T>(string sql, object? sqlParams = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Query<T>(sql, sqlParams);

    }
    public T LoadDataSingle<T>(string sql, object? sqlParams = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.QuerySingle<T>(sql, sqlParams);

    }

    public bool ExecuteSql(string sql, object? sqlParams = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Execute(sql, sqlParams) > 0;  
    }

    public int ExecuteSqlWithRowCount(string sql, object? sqlParams = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Execute(sql, sqlParams);  
    }
}