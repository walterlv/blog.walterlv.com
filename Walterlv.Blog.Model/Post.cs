using System;

namespace Walterlv.Blog
{
    public class Post
    {
        public Post(string id, string title, DateTimeOffset publishTime, DateTimeOffset updateTime, string content)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PublishTime = publishTime;
            UpdateTime = updateTime;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Id { get; }

        public string Title { get; }

        public DateTimeOffset UpdateTime { get; }

        public DateTimeOffset PublishTime { get; }

        public string Content { get; }
    }
}
