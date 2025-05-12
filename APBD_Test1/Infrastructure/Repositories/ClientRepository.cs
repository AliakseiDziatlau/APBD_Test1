using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DatabaseUtils;
using APBD_Test1.Infrastructure.Mappers;
using APBD_Test1.Infrastructure.SqlCommands;
using APBD_Test1.Infrastructure.SqlExtensions;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Client?> GetCustomerByIdAsync(int customerId)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(ClientSqlCommands.GetClientById)
            .WithParameter("@ClientId", customerId)
            .ExecuteReaderAsync();

        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapClient);
    }
}