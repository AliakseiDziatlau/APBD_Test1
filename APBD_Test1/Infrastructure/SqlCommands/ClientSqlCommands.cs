namespace APBD_Test1.Infrastructure.SqlCommands;

public static class ClientSqlCommands
{
    public static string GetClientById = 
        @"Select * from Client where client_id = @ClientId";
}