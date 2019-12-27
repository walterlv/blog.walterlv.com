using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class PostStaticRedirector
    {
        public PostStaticRedirector(string host)
        {
            Host = string.IsNullOrWhiteSpace(host) ? "https://s.blog.walterlv.com:8000" : host;
            SHost = Host;
        }

        public string Host { get; }

        public static string SHost { get; private set; }
    }
}
