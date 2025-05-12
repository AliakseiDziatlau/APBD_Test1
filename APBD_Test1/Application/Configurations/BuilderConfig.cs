namespace APBD_Test1.Application.Configurations;

public static class BuilderConfig
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        return services;
    }
}