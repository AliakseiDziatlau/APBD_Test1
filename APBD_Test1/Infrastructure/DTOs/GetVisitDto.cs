namespace APBD_Test1.Infrastructure.DTOs;

public class GetVisitDto
{
    public DateTime Date { get; set; }
    public ClientDto Client { get; set; }
    public MechanicDto Mechanic { get; set; }
    public List<ServiceDto> VisitServices { get; set; }
}