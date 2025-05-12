using APBD_Test1.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfigurations()
    .AddConnectionStrings()
    .AddDependencies();

var app = builder.Build();

app.ConfigureSwagger().MapControllers();
app.Run();
