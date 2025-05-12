using APBD_Test1.Domain.Models;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Domain.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetCustomerByIdAsync(int customerId);
}