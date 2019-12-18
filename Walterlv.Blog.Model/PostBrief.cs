using System;

namespace Walterlv.Blog.Data
{
    public class PostBrief
    {
        public PostBrief(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            UpdateTime = post.UpdateTime;
            PublishTime = post.PublishTime;
            Summary = post.Summary;
        }

        public string Id { get; }
        public string Title { get; }
        public DateTimeOffset UpdateTime { get; }
        public DateTimeOffset PublishTime { get; }
        public string Summary { get; }
    }
}
