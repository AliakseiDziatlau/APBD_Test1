namespace APBD_Test1.Infrastructure.SqlCommands;

public static class VisitSqlCommands
{
    public static string GetVisitById = 
        @"Select v.date as Date, c.first_name as ClientFirstName, c.last_name as ClientLastName, c.date_of_birth as ClientDateOfBirth, m.mechanic_id as MechanicId, m.licence_number as LicenceNumber
        from Visit v
        join Client c on v.client_id = c.client_id
        join Mechanic m on m.mechanic_id = v.mechanic_id
        where v.visit_id = @VisitId";

    public static string GetVisitByIdForCheck = 
        @"Select * from Visit where visit_id = @VisitId";

    public static string CreateVisit = 
        @"Insert into Visit (visit_id, client_id, mechanic_id, date)
        values (@VisitId, @ClientId, @MechanicId, GETDATE())";

    public static string CreateVisitService = 
        @"Insert into Visit_Service (visit_id, service_id, service_fee)
        values (@VisitId, @ServiceId, @ServiceFee)";
}