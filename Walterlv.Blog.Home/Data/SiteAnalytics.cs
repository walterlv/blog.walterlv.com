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
        private readonly ConcurrentDictionary<string, string> _titleMarks = new ConcurrentDictionary<string, string>();

        public void Mark(string urlPath, string title)
        {
            _titleMarks.AddOrUpdate(urlPath, title, (x, y) => title);
        }

        public void Record(string urlPath)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(urlPath);
            Console.ResetColor();

            if (_titleMarks.TryGetValue(urlPath, out var title))
            {
                Console.ResetColor();
                Console.WriteLine($"           {title}");
            }
        }
    }
}
