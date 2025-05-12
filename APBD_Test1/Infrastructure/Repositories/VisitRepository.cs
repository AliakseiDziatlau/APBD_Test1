using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DatabaseUtils;
using APBD_Test1.Infrastructure.DTOs;
using APBD_Test1.Infrastructure.Mappers;
using APBD_Test1.Infrastructure.SqlCommands;
using APBD_Test1.Infrastructure.SqlExtensions;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly string _connectionString;

    public VisitRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<GetVisitDto?> GetVisitByIdAsync(int visitId)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(VisitSqlCommands.GetVisitById)
            .WithParameter("@VisitId", visitId)
            .ExecuteReaderAsync();
        
        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapGetVisitDto);
    }

    public async Task<Visit?> GetVisitByIdForCheckAsync(int visitId)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(VisitSqlCommands.GetVisitByIdForCheck)
            .WithParameter("@VisitId", visitId)
            .ExecuteReaderAsync();

        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapVisit);
    }

    // public async Task CreateVisitAsync(int VisitId, int ClientId, int MechanicId)
    // {
    //     await FluentSql
    //         .From(connection, transaction)
    //         .WithSql(VisitSqlCommands.CreateVisit)
    //         .WithParameters(
    //             ("@VisitId", VisitId),
    //             ("@ClientId", ClientId),
    //             ("@MechanicId", MechanicId))
    //         .ExecuteNonQueryAsync();
    // }
    //
    // public async Task CreateVisitServiceAsync(int VisitId, int ServiceId, decimal ServiceFee)
    // {
    //     await FluentSql
    //         .From(connection, transaction)
    //         .WithSql(VisitSqlCommands.CreateVisitService)
    //         .WithParameters(
    //             ("@VisitId", VisitId),
    //             ("@ServiceId", ServiceId),
    //             ("@ServiceFee", ServiceFee))
    //         .ExecuteNonQueryAsync();
    // }

    public async Task CreateVisitAndServiceVisit(int VisitId, int ClientId, int MechanicId, List<(int serviceId, decimal serviceFee)> services)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

        try
        {
            await FluentSql
                .From(connection, transaction)
                .WithSql(VisitSqlCommands.CreateVisit)
                .WithParameters(
                    ("@VisitId", VisitId),
                    ("@ClientId", ClientId),
                    ("@MechanicId", MechanicId))
                .ExecuteNonQueryAsync();

            foreach (var service in services)
            {
                await FluentSql
                    .From(connection, transaction)
                    .WithSql(VisitSqlCommands.CreateVisitService)
                    .WithParameters(
                        ("@VisitId", VisitId),
                        ("@ServiceId", service.serviceId),
                        ("@ServiceFee", service.serviceFee))
                    .ExecuteNonQueryAsync();
            }
            
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}