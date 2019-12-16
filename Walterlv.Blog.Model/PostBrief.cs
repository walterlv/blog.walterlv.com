using System;

namespace Walterlv.Blog.Data
{
    public class PostBrief
    {
        public PostBrief(string id, string title)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Id { get; }

        public string Title { get; }

        public string? UpdateTime { get; set; }
        
        public string? PublishTime { get; set; }

        public string? Summary { get; set; }
    }
}
