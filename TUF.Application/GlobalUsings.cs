global using MediatR;
global using DTO = TUF.Client.Shared.Dtos;
global using DB = TUF.Database.TUFDB;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Daniel.Common.Models;

global using Mapster;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;

public static class Bootstrap
{
    public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}