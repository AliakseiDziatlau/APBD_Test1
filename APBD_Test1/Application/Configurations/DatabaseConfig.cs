namespace APBD_Test1.Application.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddConnectionStrings(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
            sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"));
        return services;
    }
}