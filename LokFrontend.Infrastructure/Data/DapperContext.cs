using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
namespace LokFrontend.Infrastructure.Data;

public class PsqlDb : IDisposable
{
    private readonly IDbConnection _connection;

    public PsqlDb(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
    }

    public IDbConnection GetConnection() => _connection;

    public void Dispose()
    {
        _connection.Close();
    }
}

public interface IConnectionString
{
    string GetConnectionString();
    void SetConnectionString(string connectionString);
}

public class DapperContext
{
    protected IConnectionString _connectionString { get; }

    public DapperContext(IConnectionString connectionString)
    {
        _connectionString = connectionString;

    }
    public async Task<List<T>> LoadData<T>(string sql)
    {
        using var db = GetDb();
        using var connection = db.GetConnection();
        //using var connection =new PsqlDb(_connectionString.GetConnectionString()).GetConnection();

        var rows = await connection.QueryAsync<T>(sql);
        return rows.ToList();
    }

    public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
    {
        using var db = GetDb();
        using var connection = db.GetConnection();
        //using var connection =new PsqlDb(_connectionString.GetConnectionString()).GetConnection();
        connection.Open();
        var rows = await connection.QueryAsync<T>(sql, parameters);
        return rows.ToList();
    }

    public async Task<T> LoadSingleData<T, U>(string sql, U parameters)
    {
        using var db = GetDb();
        using var connection = db.GetConnection();
        //using var connection =new PsqlDb(_connectionString.GetConnectionString()).GetConnection();
        var rows = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        return rows;
    }

    public Task SaveData<T>(string sql, T parameters)
    {
        //using var connection =new PsqlDb(_connectionString.GetConnectionString()).GetConnection();
        using var db = GetDb();
        using var connection = db.GetConnection();
        var response = connection.ExecuteAsync(sql, parameters);
        return response;
    }

    public Task<int> SaveMany<T>(string sql, List<T> parameters)
    {
        using var db = GetDb();
        using var connection = db.GetConnection();
        connection.Open();
        var trans = connection.BeginTransaction();
        var returned = connection.ExecuteAsync(sql, parameters, transaction: trans);
        trans.Commit();
        connection.Close();
        return returned;
    }

    public Task<T> GetLastInsertId<T>()
    {
        return LoadSingleData<T, object>("select last_insert_id()", new { });
    }

    private PsqlDb GetDb()
    {
        return new PsqlDb(_connectionString.GetConnectionString());
    }
}