using System;

namespace Walterlv.Blog
{
    public class Page
    {
        public Page()
        {
        }

        public Page(string url, string title, string content)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Url { get; set; } = "";
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
    }
}
