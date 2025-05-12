using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Domain.Interfaces;

public interface IVisitRepository
{
    Task<GetVisitDto?> GetVisitByIdAsync(int visitId);
    Task<Visit?> GetVisitByIdForCheckAsync(int visitId);
    // Task CreateVisitAsync(int VisitId, int ClientId, int MechanicId);
    // Task CreateVisitServiceAsync(int VisitId, int ServiceId, decimal ServiceFee);

    Task CreateVisitAndServiceVisit(int VisitId, int ClientId, int MechanicId,
        List<(int serviceId, decimal serviceFee)> services);
}