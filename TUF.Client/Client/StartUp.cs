using Blazored.LocalStorage;
using Daniel.Common.Interfaces;
using MediatR.Courier;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using TUF.Client.Client.Shared;
using TUF.Client.Infra.Services;
using TUF.Client.Shared.Authorization;
using MudBlazor.Services;
using MudBlazor;
using MediatR.Courier.DependencyInjection;
using TUF.Client.Infra.Common;
using TUF.Client.Infra.Notifications;
using TUF.Client.Infra.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace TUF.Client.Client;

public static class StartUp
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration config) =>
        services.AddBlazoredLocalStorage()
        .AddMudServices(configuration =>
        {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            configuration.SnackbarConfiguration.HideTransitionDuration = 100;
            configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
            configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
            configuration.SnackbarConfiguration.ShowCloseIcon = false;
        })
        //.AddServices(typeof(ITransient), ServiceLifetime.Transient)
        //.AutoRegisterInterfaces<ITransient>()
        .AddTransient<ITokenService, TokenService>()
        .AddTransient<IApiHelper, ApiHelper>()
        .AddAuthentication(config)
        .AddNotifications()
        .AddAuthorizationCore(RegisterPermissionClaims);

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        //services
        //        .AddScoped<AuthenticationStateProvider, JwtAuthenticationService>()
        //        .AddScoped(sp => (IAuthenticationService)sp.GetRequiredService<AuthenticationStateProvider>())
        //        .AddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>())
        //        .AddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>()
        //        .AddScoped<JwtAuthenticationHeaderHandler>();

        services.AddScoped<JwtAuthenticationService>();
        services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationService>());
        return services;
    }
    private static void RegisterPermissionClaims(AuthorizationOptions options)
    {
        foreach (var permission in TUFPermissions.All)
        {
            options.AddPolicy(permission.Name, policy => policy.RequireClaim(TUFClaims.Permission, permission.Name));
        }
    }

    private static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
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
    private static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
   lifetime switch
   {
       ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
       ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
       ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
       _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
   };

    

}
