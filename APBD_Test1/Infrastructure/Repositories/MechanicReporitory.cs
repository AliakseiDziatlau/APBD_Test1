using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DatabaseUtils;
using APBD_Test1.Infrastructure.Mappers;
using APBD_Test1.Infrastructure.SqlCommands;
using APBD_Test1.Infrastructure.SqlExtensions;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.Repositories;

public class MechanicReporitory : IMechanicRepository
{
    private readonly string _connectionString;

    public MechanicReporitory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<Mechanic?> GetMechanicByLicenceNumberAsync(string LicenceNumber)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(MechanicSqlCommands.GetMechanicById)
            .WithParameter("@licenceNumber", LicenceNumber)
            .ExecuteReaderAsync();

        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapMechanic);
    }
}