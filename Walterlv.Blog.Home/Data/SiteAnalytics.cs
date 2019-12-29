using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class SiteAnalytics
    {
        public void Handle(string urlPath, string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}] [Loaded] ");
            Console.WriteLine(urlPath);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"                    {message}");
        }

        public void Route(string urlPath)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}] [Routed] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(urlPath);
            Console.ResetColor();
        }
    }
}
