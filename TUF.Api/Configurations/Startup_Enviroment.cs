

using Daniel.Common.Services;
using Finbuckle.MultiTenant;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TUF.Api.Configurations;
using TUF.Api.Infra.Caching;
using TUF.Api.Infra.Cors;
using TUF.Api.Infra.OpenApi;
using TUF.Api.Infra.SecurityHeaders;
using TUF.Client.Shared;
using TUF.Database.DbContexts;

namespace TUF.HostApi.Configurations;

internal static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly)
            .AddMediatR(assembly)
            .AddApplicationInfrastructure(config);
        return services
            .AddApiVersioning()
            .ServiceResist(config)            
            .AddCaching(config)
            //.AddHealthCheck()
            .AddCorsPolicy(config)
            .AddOpenApiDocumentation(config)
            .AddAuth(config)
            //.AddNotifications(config)
            .AddRouting(options => options.LowercaseUrls = true);
    }
    public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, SystemDateTimeService>();
        services.AddTransient<ICrytoManager, CrytoManager>();
        //services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddDbContext<IdentityContext>(options =>
        options.UseSqlServer(configuration.GetSection("DatabaseSettings:IdentityConnection").Value)
        , ServiceLifetime.Transient);

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            configuration.GetSection("DatabaseSettings:ApplicationConnection").Value)
        , ServiceLifetime.Transient);

        services.AddDbContext<TenantDbContext>(options =>
        options.UseSqlServer(
            configuration.GetSection("DatabaseSettings:ApplicationConnection").Value)
        , ServiceLifetime.Transient);

        
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        return builder
                   .UseRequestLocalization()
                   .UseStaticFiles()
                   .UseSecurityHeaders(config)
                   //.UseFileStorage()
                   //.UseExceptionMiddleware()
                   .UseHttpsRedirection()
                   .UseRouting()
                   .UseCorsPolicy()
                   .UseAuthentication()
                   .UseAuthorization()
                   .UseCurrentUser()
                   //.UseMultiTenancy() 
                   //.UseRequestLogging(config)
                   //.UseHangfireDashboard(config)
                   .UseOpenApiDocumentation(config);
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        //builder.MapHealthCheck();
        //builder.MapNotifications();
        return builder;
    }

 
}
