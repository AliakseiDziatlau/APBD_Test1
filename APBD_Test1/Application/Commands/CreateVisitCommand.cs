using APBD_Test1.Infrastructure.DTOs;
using MediatR;

namespace APBD_Test1.Application.Commands;

public class CreateVisitCommand : IRequest<(bool success, string message)>
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public string MechanicLicenceNumber { get; set; }
    public List<AddServiceDto> Services { get; set; }
}