using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DatabaseUtils;
using APBD_Test1.Infrastructure.DTOs;
using APBD_Test1.Infrastructure.Mappers;
using APBD_Test1.Infrastructure.SqlCommands;
using APBD_Test1.Infrastructure.SqlExtensions;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly string _connectionString;

    public ServiceRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<List<ServiceDto>> GetAllServicesByVisitIdAsync(int visitId)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(ServiceSqlCommands.GetServicesByVisitId)
            .WithParameter("@VisitId", visitId)
            .ExecuteReaderAsync();
        
        return await DbUtils.MapListAsync(reader, SqlMapper.MapServiceDto);
    }
    
    public async Task<Service?> GetServiceByNameAsync(string name)
    {
        await using var reader  = await FluentSql
            .From(_connectionString)
            .WithSql(ServiceSqlCommands.GetServiceByName)
            .WithParameter("@name", name)
            .ExecuteReaderAsync();

        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapService);
    }
}