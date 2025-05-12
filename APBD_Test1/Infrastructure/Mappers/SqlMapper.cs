using APBD_Test1.Domain.Models;
using APBD_Test1.Infrastructure.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_Test1.Infrastructure.Mappers;

public static class SqlMapper
{
    public static GetVisitDto MapGetVisitDto(SqlDataReader reader)
    {
        return new GetVisitDto
        {
            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
            Client = new ClientDto
            {
                FirstName = reader.GetString(reader.GetOrdinal("ClientFirstName")),
                LastName = reader.GetString(reader.GetOrdinal("ClientLastName")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("ClientDateOfBirth")),
            },
            Mechanic = new MechanicDto
            {
                MechanicId = reader.GetInt32(reader.GetOrdinal("MechanicId")),
                LicenceNumber = reader.GetString(reader.GetOrdinal("LicenceNumber")),
            },
            VisitServices = new List<ServiceDto>()
        };
    }

    public static ServiceDto MapServiceDto(SqlDataReader reader)
    {
        return new ServiceDto
        {
            Name = reader.GetString(reader.GetOrdinal("Name")),
            ServiceFee = reader.GetDecimal(reader.GetOrdinal("Fee")),
        };
    }

    public static Visit MapVisit(SqlDataReader reader)
    {
        return new Visit
        {
            VisitId = reader.GetInt32(reader.GetOrdinal("visit_id")),
            ClientId = reader.GetInt32(reader.GetOrdinal("client_id")),
            MechanicId = reader.GetInt32(reader.GetOrdinal("mechanic_id")),
            Date = reader.GetDateTime(reader.GetOrdinal("date")),
        };
    }

    public static Client MapClient(SqlDataReader reader)
    {
        return new Client
        {
            ClientId = reader.GetInt32(reader.GetOrdinal("client_id")),
            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
            LastName = reader.GetString(reader.GetOrdinal("last_name")),
            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
        };
    }

    public static Mechanic MapMechanic(SqlDataReader reader)
    {
        return new Mechanic
        {
            MechanicId = reader.GetInt32(reader.GetOrdinal("mechanic_id")),
            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
            LastName = reader.GetString(reader.GetOrdinal("last_name")),
            LicenceNumber = reader.GetString(reader.GetOrdinal("licence_number")),
        };
    }
    
    public static Service MapService(SqlDataReader reader)
    {
        return new Service
        {
            ServiceId = reader.GetInt32(reader.GetOrdinal("service_id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            BaseFee = reader.GetDecimal(reader.GetOrdinal("base_fee")),
        };
    }
}