namespace APBD_Test1.Infrastructure.SqlCommands;

public static class MechanicSqlCommands
{
    public static string GetMechanicById = 
        @"Select * from Mechanic where licence_number = @licenceNumber";
}