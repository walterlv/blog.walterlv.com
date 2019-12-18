using System;

namespace Walterlv.Blog
{
    public class Post
    {
        public Post()
        {
        }

        public Post(string id, string title, DateTimeOffset publishTime, DateTimeOffset updateTime, string summary, string content, bool isPublished)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PublishTime = publishTime;
            UpdateTime = updateTime;
            Summary = summary;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            IsPublished = isPublished;
        }

        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset PublishTime { get; set; } = DateTimeOffset.MinValue;
        public string Summary { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsPublished { get; set; } = true;
    }
}
