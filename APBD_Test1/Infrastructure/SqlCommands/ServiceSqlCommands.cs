namespace APBD_Test1.Infrastructure.SqlCommands;

public static class ServiceSqlCommands
{
    public static string GetServicesByVisitId = 
        @"Select s.name as Name, vs.service_fee as Fee
        from Service s
        join Visit_Service vs on s.service_id = vs.service_id
        where vs.visit_id = @VisitId";
    
    public static string GetServiceByName = 
        @"Select * from Service where name = @name";
}