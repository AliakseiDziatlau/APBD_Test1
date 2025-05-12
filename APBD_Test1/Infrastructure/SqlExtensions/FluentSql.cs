using System.Data;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.SqlExtensions;

public class FluentSql : IAsyncDisposable
{
    private readonly SqlConnection _connection;
    private readonly SqlCommand _command;
    private readonly bool _shouldDisposeConnection;

    private FluentSql(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _command = _connection.CreateCommand();
        _command.CommandType = CommandType.Text;
        _shouldDisposeConnection = true;
    }
    
    private FluentSql(SqlConnection connection, SqlTransaction transaction)
    {
        _connection = connection;
        _command = connection.CreateCommand();
        _command.CommandType = CommandType.Text;
        _command.Transaction = transaction;
        _shouldDisposeConnection = false;
    }

    public static FluentSql From(string connectionString)
    {
        return new FluentSql(connectionString);
    }
    
    public static FluentSql From(SqlConnection connection, SqlTransaction transaction)
    {
        return new FluentSql(connection, transaction);
    }

    public FluentSql WithSql(string sql)
    {
        _command.CommandText = sql;
        return this;
    }

    public FluentSql WithParameter(string name, object? value)
    {
        var param = _command.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        _command.Parameters.Add(param);
        return this;
    }
    
    public FluentSql WithParameters(params (string name, object? value)[] parameters)
    {
        foreach (var (name, value) in parameters)
        {
            var param = _command.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            _command.Parameters.Add(param);
        }

        return this;
    }

    public async Task<SqlDataReader> ExecuteReaderAsync()
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        return await _command.ExecuteReaderAsync();
    }
    
    public async Task<object?> ExecuteScalarAsync()
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        return await _command.ExecuteScalarAsync();
    }
    
    public async Task ExecuteNonQueryAsync()
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        await _command.ExecuteNonQueryAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _shouldDisposeConnection
            ? _connection.DisposeAsync()
            : ValueTask.CompletedTask;
    }
}