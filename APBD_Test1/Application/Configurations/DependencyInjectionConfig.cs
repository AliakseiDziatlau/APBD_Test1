using APBD_Test1.Domain.Interfaces;
using APBD_Test1.Infrastructure.Repositories;
using MediatR;

namespace APBD_Test1.Application.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program));
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IMechanicRepository, MechanicReporitory>();
        return services;
    }
}