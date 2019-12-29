using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dotnetCampus.Cli;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Walterlv.Blog.Data;
using Walterlv.Blog.Middlewares;

namespace Walterlv.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<HttpClient>();
            services.AddSingleton(sp =>
            {
                var options = CommandLine.Parse(Environment.GetCommandLineArgs().Skip(1).ToArray()).As<Options>();
                return new PostStaticRedirector(options.StaticHost);
            });
            services.AddSingleton(sp => new PostGenerator(sp.GetService<PostStaticRedirector>()));
            services.AddSingleton(new SiteAnalytics());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAnalyticsButExcluding("/img", "/css", "/js", "/_framework", "/_blazor");
            app.UseDomainRedirection("blog.walterlv.com", "localhost");
            app.UseHttpsRedirection();
            app.UseExternalHttpsRedirection();
            app.UsePostLegacyUrlRedirection();
            app.UseAutoRemoveHtmlExtension();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
        }
    }
}
