using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;

namespace Walterlv.Blog.Middlewares
{
    public static class RedirectExtensions
    {
        /// <summary>
        /// 永久重定向 http 到 https。用于解决 <see cref="HttpsPolicyBuilderExtensions.UseHttpsRedirection(IApplicationBuilder)"/> 无法真的重定向的问题。
        /// 注意，此方法不会对 localhost / 127.0.0.1 / [::1] 重定向。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>。</param>
        /// <returns><see cref="IApplicationBuilder"/>。</returns>
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

        /// <summary>
        /// 自动移除所有的 .html 后缀，并永久重定向到没有 .html 后缀的网页。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>。</param>
        /// <returns><see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseAutoRemoveHtmlExtension(this IApplicationBuilder app) => app.Use(async (context, next) =>
        {
            var urlPath = context.Request.Path.HasValue
                ? context.Request.Path.Value
                : "";
            if (urlPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            {
                // 去掉 .html 后缀
                var url = context.Request.Host + context.Request.PathBase + urlPath[0..^5];
                context.Response.Redirect(url);
                context.Response.StatusCode = 301;
                return;
            }
            await next().ConfigureAwait(false);
        });

        /// <summary>
        /// 如果有某一些网页打算废弃，那么请使用此中间件将其永久重定向到新的网页中。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>。</param>
        /// <param name="pathRedirections">
        /// 如果你有多个希望重定向的 URL，请像这样传递参数：
        /// <para>"/path/legacy1", "/path/new1", "/path/legacy2", "/path/new2"</para>
        /// </param>
        /// <returns><see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseLegacyUrlsRedirection(this IApplicationBuilder app, params string[] pathRedirections)
        {
            if (pathRedirections is null)
            {
                throw new ArgumentNullException(nameof(pathRedirections));
            }

            if (pathRedirections.Length % 2 != 0)
            {
                throw new ArgumentException("参数传入的规则是 (legacyUrl1, newUrl1, legacyUrl2, newUrl2)，因此参数数量必须是偶数个。", nameof(pathRedirections));
            }

            var mapping = new Dictionary<string, string>();
            for (var i = 0; i < pathRedirections.Length; i += 2)
            {
                mapping.Add(pathRedirections[i], pathRedirections[i + 1]);
            }

            return app.Use(async (context, next) =>
            {
                var legacyUrl = context.Request.Path.HasValue
                    ? context.Request.Path.Value
                    : "";
                if (mapping.TryGetValue(legacyUrl, out var newUrl))
                {
                    var url = "//" + context.Request.Host + context.Request.PathBase + newUrl;
                    context.Response.Redirect(url);
                    context.Response.StatusCode = 301;
                    return;
                }
                await next().ConfigureAwait(false);
            });
        }
    }
}
