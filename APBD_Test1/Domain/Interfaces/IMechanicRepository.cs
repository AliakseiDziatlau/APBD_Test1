using APBD_Test1.Domain.Models;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Domain.Interfaces;

public interface IMechanicRepository
{
    Task<Mechanic?> GetMechanicByLicenceNumberAsync(string LicenceNumber);
}