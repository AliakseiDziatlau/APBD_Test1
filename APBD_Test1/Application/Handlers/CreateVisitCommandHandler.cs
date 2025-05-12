using APBD_Test1.Application.Commands;
using APBD_Test1.Domain.Interfaces;
using MediatR;

namespace APBD_Test1.Application.Handlers;

public class CreateVisitCommandHandler : IRequestHandler<CreateVisitCommand, (bool success, string message)>
{
    private readonly IVisitRepository _visitRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IMechanicRepository _mechanicRepository;
    private readonly IServiceRepository _serviceRepository;

    public CreateVisitCommandHandler(
        IVisitRepository visitRepository,
        IClientRepository clientRepository,
        IMechanicRepository mechanicRepository,
        IServiceRepository serviceRepository)
    {
        _visitRepository = visitRepository;
        _clientRepository = clientRepository;
        _mechanicRepository = mechanicRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<(bool success, string message)> Handle(CreateVisitCommand request, CancellationToken cancellationToken)
    {
        var visit = await _visitRepository.GetVisitByIdForCheckAsync(request.VisitId);
        if (visit is not null)
            return (false, "Visit already exists");
            
        var client = await _clientRepository.GetCustomerByIdAsync(request.ClientId);
        if (client is null)
            return (false, "Client not found");
            
        var mechanic = await _mechanicRepository.GetMechanicByLicenceNumberAsync(request.MechanicLicenceNumber);
        if (mechanic is null)
            return (false, "Mechanic not found");

        var services = new List<(int id, decimal price)>();
        foreach (var item in request.Services)
        {
            var service = await _serviceRepository.GetServiceByNameAsync(item.ServiceName);
            if (service is null)
                return (false, $"Service with name {item.ServiceName} does not exist");
            
            services.Add((service.ServiceId, item.ServiceFee));
        }
            
        await _visitRepository.CreateVisitAndServiceVisit(request.VisitId, request.ClientId, mechanic.MechanicId, services);
        return (true, "Visit created");
    }
}