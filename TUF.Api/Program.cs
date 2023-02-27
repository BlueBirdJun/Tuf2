using Serilog;
using TUF.Api.Controllers;
using TUF.HostApi.Configurations;

[assembly: ApiConventionType(typeof(TUFApiConventions))]

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.AddConfigurations();
builder.Host.UseSerilog((_, config) =>
{
    config.WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddControllersWithViews();//.AddFluentValidation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistenceContexts(builder.Configuration);

var app = builder.Build();
app.UseInfrastructure(builder.Configuration);
app.MapEndpoints();
app.Run();
 