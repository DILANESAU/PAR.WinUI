using Dapper;
using Microsoft.Data.SqlClient;

namespace PAR.Api.Services
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<T>> QueryAsync<T>(string sql, object? param = null)
        {
            using ( var connection = new SqlConnection(_connectionString) )
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(sql, param, commandTimeout: 300);
                return result.AsList();
            }
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        {
            using ( var connection = new SqlConnection(_connectionString) )
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: 300);
            }
        }
        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using ( var connection = new SqlConnection(_connectionString) )
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(sql, param, commandTimeout: 300);
            }
        }
    }
}