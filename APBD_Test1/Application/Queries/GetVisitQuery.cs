using APBD_Test1.Infrastructure.DTOs;
using MediatR;

namespace APBD_Test1.Application.Queries;

public class GetVisitQuery : IRequest<(bool isSuccess, GetVisitDto? dto)>
{
    public int Id { get; set; }
}