using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextDapper
    {
        // Connecting to database
        // It has the meta data of our connection
        // We are making private so we can use in the class only 
        private string? _connectionString;
        public DataContextDapper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        
        
        // Creating Method for loading data which expects IEnum as return type
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }
        
        public T LoadSingleData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);            
        }
        
        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql) > 0);            
        }
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);            
        }
    }
}