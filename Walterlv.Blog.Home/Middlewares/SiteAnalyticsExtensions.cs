using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using Walterlv.Blog.Data;

namespace Walterlv.Blog.Middlewares
{
    public static class SiteAnalyticsExtensions
    {
        /// <summary>
        /// 其他中间件没有处理掉的页面将进入此中间件进行通用的数据收集。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>。</param>
        /// <param name="prefixes">所有不被统计的前缀（例如 css/js 等页面必须元素一般不参与统计），写作 "/img", "/css", "/js"。</param>
        /// <returns><see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseAnalyticsButExcluding(this IApplicationBuilder app, params string[] prefixes)
        {
            var pathPrefixes = prefixes.ToList();
            return app.Use(async (context, next) =>
            {
                var path = context.Request.Path.HasValue
                    ? context.Request.Path.Value
                    : "";
                if (pathPrefixes.Find(x => path.StartsWith(x, StringComparison.OrdinalIgnoreCase)) == null)
                {
                    var analytics = (SiteAnalytics)context.RequestServices.GetService(typeof(SiteAnalytics));
                    analytics.Route(context.Request.Path);
                }
                await next().ConfigureAwait(false);
            });
        }
    }
}
