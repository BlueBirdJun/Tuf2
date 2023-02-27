using Daniel.Common.Interfaces;
using TUF.Api.Configurations;

namespace TUF.HostApi.Configurations;

internal static partial class Startup
{


    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        return services
        .AddCurrentUser()
        .AddJwtAuth(config)
        .AddIdentity();
        //.AddPermissions(); 
    }
   

}
