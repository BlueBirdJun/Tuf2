﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Api.Infra.SecurityHeaders;

public static class Startup
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, IConfiguration config)
    {
        var settings = config.GetSection(nameof(SecurityHeaderSettings)).Get<SecurityHeaderSettings>();
        settings.Enable = true;
        settings.XFrameOptions = "SAMEORIGIN";
        settings.XContentTypeOptions = "nosniff";
        settings.ReferrerPolicy = "same-origin";
        settings.PermissionsPolicy = "geolocation=(), camera=()";
        settings.SameSite = "";
        settings.XXSSProtection = "1; mode=block";

        if (settings?.Enable is true)
        {
            app.Use(async (context, next) =>
            {
                if (!context.Response.HasStarted)
                {
                    if (!string.IsNullOrWhiteSpace(settings.XFrameOptions))
                    {
                        context.Response.Headers.Add(HeaderNames.XFRAMEOPTIONS, settings.XFrameOptions);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.XContentTypeOptions))
                    {
                        context.Response.Headers.Add(HeaderNames.XCONTENTTYPEOPTIONS, settings.XContentTypeOptions);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.ReferrerPolicy))
                    {
                        context.Response.Headers.Add(HeaderNames.REFERRERPOLICY, settings.ReferrerPolicy);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.PermissionsPolicy))
                    {
                        context.Response.Headers.Add(HeaderNames.PERMISSIONSPOLICY, settings.PermissionsPolicy);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.SameSite))
                    {
                        context.Response.Headers.Add(HeaderNames.SAMESITE, settings.SameSite);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.XXSSProtection))
                    {
                        context.Response.Headers.Add(HeaderNames.XXSSPROTECTION, settings.XXSSProtection);
                    }
                }

                await next();
            });
        }
        return app;
    }
}