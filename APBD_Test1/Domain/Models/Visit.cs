namespace APBD_Test1.Domain.Models;

public class Visit
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public int MechanicId { get; set; }
    public DateTime Date { get; set; }
}