using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class SiteAnalytics
    {
        public void Record(string urlPath, string title = null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(urlPath);
            Console.ResetColor();
            if (title != null)
            {
                Console.ResetColor();
                Console.WriteLine($"           {title}");
            }
        }
    }
}
