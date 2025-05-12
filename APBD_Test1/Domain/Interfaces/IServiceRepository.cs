using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Domain.Interfaces;

public interface IServiceRepository
{
    Task<List<ServiceDto>> GetAllServicesByVisitIdAsync(int visitId);
    Task<Service?> GetServiceByNameAsync(string name);
}