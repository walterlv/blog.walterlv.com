using System;
using System.Collections.Generic;
using System.Text;

namespace Walterlv.Blog
{
    public class Post
    {
        public Post(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Id { get; set; }

        public string? Title { get; set; }

        public string? UpdateTime { get; set; }

        public string? PublishTime { get; set; }

        public string? Content { get; set; }
    }
}
