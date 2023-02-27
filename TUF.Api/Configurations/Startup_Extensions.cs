using Daniel.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;
using TUF.Api.Infra.Auth;
using TUF.Api.Infra.Auth.Jwt;
using TUF.Api.Infra.Middleware;
using TUF.Client.Shared.Authorization;
using TUF.Database.DbContexts;
using TUF.Database.Identity.Models;

namespace TUF.Api.Configurations;

internal static partial class Startup
{
    internal static IServiceCollection AddIdentity(this IServiceCollection services) =>
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            //.AddDefaultTokenProviders()
            .Services;

    internal static IServiceCollection ServiceResist(this IServiceCollection services, IConfiguration config)
    {
        return services
                 .AddServices(typeof(ITransient), ServiceLifetime.Transient)
                 .AddServices(typeof(IScoped), ServiceLifetime.Scoped);
    }
    internal static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
    {
        var interfaceTypes =
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null
                            && interfaceType.IsAssignableFrom(t.Service));

        foreach (var type in interfaceTypes)
        {
            services.AddService(type.Service!, type.Implementation, lifetime);
        }

        return services;
    }
    internal static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
   lifetime switch
   {
       ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
       ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
       ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
       _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
   };


    internal static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        var c = config.GetSection("SecuritySettings:JwtSettings:key").Value;
        byte[] key = Encoding.ASCII.GetBytes(c);         

        return services.AddAuthentication(authentication =>
        {
            authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
           .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
          .Services;
    }



    internal static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
       services
      .AddScoped<CurrentUserMiddleware>()
      .AddScoped<ICurrentUser, CurrentUser>()
      .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
   app.UseMiddleware<CurrentUserMiddleware>();
}
