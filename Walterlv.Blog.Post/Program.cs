using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Walterlv.Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 4431, listenOptions =>
                    {
                        var passward = Console.ReadLine().Trim();
                        Console.CursorTop--;
                        listenOptions.UseHttps(@"D:\WIP\Desktop\walterlv-com-iis-1213130742.pfx", passward);
                    });
                })
            .Build();
    }
}
