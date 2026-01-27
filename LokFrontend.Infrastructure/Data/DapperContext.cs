using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace LokFrontend.Infrastructure.Data;
public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    // Create and open a new DB connection
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }

    // Load list of data without parameters
    public async Task<List<T>> LoadDataAsync<T>(string sql)
    {
        using var connection = CreateConnection();
        var rows = await connection.QueryAsync<T>(sql);
        return rows.ToList();
    }

    // Load list of data with parameters
    public async Task<List<T>> LoadDataAsync<T, U>(string sql, U parameters)
    {
        using var connection = CreateConnection();
        var rows = await connection.QueryAsync<T>(sql, parameters);
        return rows.ToList();
    }

    // Load single row with parameters
    public async Task<T> LoadSingleDataAsync<T, U>(string sql, U parameters)
    {
        using var connection = CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        return row;
    }

    // Save data (insert/update/delete)
    public async Task<int> SaveDataAsync<T>(string sql, T parameters)
    {
        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, parameters);
        return affectedRows;
    }

    // Save many records in a transaction
    public async Task<int> SaveManyAsync<T>(string sql, List<T> parameters)
    {
        using var connection = CreateConnection();
        using var transaction = connection.BeginTransaction();

        var affectedRows = await connection.ExecuteAsync(sql, parameters, transaction);
        transaction.Commit();

        return affectedRows;
    }

    // Get last inserted ID (Postgres uses RETURNING, so this is optional)
    // This is a MySQL specific example; in Postgres, use "RETURNING id" in insert queries instead
    public async Task<T> GetLastInsertIdAsync<T>()
    {
        using var connection = CreateConnection();
        var lastId = await connection.QuerySingleAsync<T>("SELECT LASTVAL()");
        return lastId;
    }
}
