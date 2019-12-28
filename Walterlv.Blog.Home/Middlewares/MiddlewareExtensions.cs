using Microsoft.AspNetCore.Builder;
using System;

namespace Walterlv.Blog.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExternalHttpsRedirection(this IApplicationBuilder app) => app.Use(async (context, next) =>
        {
            if (!context.Request.IsHttps
                && "localhost" != context.Request.Host.Host
                && "127.0.0.1" != context.Request.Host.Host
                && "[::1]" != context.Request.Host.Host)
            {
                // HTTP 重定向为 HTTPS
                var url = "https://" + context.Request.Host + context.Request.PathBase + context.Request.Path;
                context.Response.Redirect(url);
                context.Response.StatusCode = 301;
                return;
            }
            await next().ConfigureAwait(false);
        });

        public static IApplicationBuilder UseAutoRemoveHtmlExtension(this IApplicationBuilder app) => app.Use(async (context, next) =>
        {
            if (context.Request.Path.HasValue
                && context.Request.Path.Value.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            {
                // 去掉 .html 后缀
                var url = "https://" + context.Request.Host + context.Request.PathBase + context.Request.Path;
                context.Response.Redirect(url);
                context.Response.StatusCode = 301;
                return;
            }
            await next().ConfigureAwait(false);
        });
    }
}
