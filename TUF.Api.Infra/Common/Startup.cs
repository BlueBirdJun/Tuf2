using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Api.Infra.Caching;
using TUF.Api.Infra.Common.Caching;

namespace TUF.Infrastructure.Common;

internal static class Startup
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
        //if (settings.UseDistributedCache)
        //{
        //    if (settings.PreferRedis)
        //    {
        //        services.AddStackExchangeRedisCache(options =>
        //        {
        //            options.Configuration = settings.RedisURL;
        //            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
        //            {
        //                AbortOnConnectFail = true,
        //                EndPoints = { settings.RedisURL }
        //            };
        //        });
        //    }
        //    else
        //    {
        //        services.AddDistributedMemoryCache();
        //    }

        //    services.AddTransient<ICacheService, DistributedCacheService>();
        //}
        //else
        //{
        //    services.AddMemoryCache();
        //    services.AddTransient<ICacheService, LocalCacheService>();
        //}
        services.AddMemoryCache();
        services.AddTransient<ICacheService, LocalCacheService>();
        return services;
    }

}
