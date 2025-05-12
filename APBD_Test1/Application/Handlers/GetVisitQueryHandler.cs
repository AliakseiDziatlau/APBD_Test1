using APBD_Test1.Application.Queries;
using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Infrastructure.DTOs;
using MediatR;

namespace APBD_Test1.Application.Handlers;

public class GetVisitQueryHandler : IRequestHandler<GetVisitQuery, (bool isSuccess, GetVisitDto? dto)>
{
    private readonly IVisitRepository _visitRepository;
    private readonly IServiceRepository _serviceRepository;

    public GetVisitQueryHandler(IVisitRepository visitRepository, IServiceRepository serviceRepository)
    {
        _visitRepository = visitRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<(bool isSuccess, GetVisitDto? dto)> Handle(GetVisitQuery request, CancellationToken cancellationToken)
    {
        var visits = await _visitRepository.GetVisitByIdAsync(request.Id);
        if (visits is null)
            return (false, null);
        
        visits.VisitServices = await _serviceRepository.GetAllServicesByVisitIdAsync(request.Id);
        return (true, visits);
    }
}