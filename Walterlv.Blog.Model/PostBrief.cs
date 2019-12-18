using System;

namespace Walterlv.Blog.Data
{
    public class PostBrief
    {
        public PostBrief()
        {
        }

        public PostBrief(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            UpdateTime = post.UpdateTime;
            PublishTime = post.PublishTime;
            Summary = post.Summary;
        }

        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset PublishTime { get; set; } = DateTimeOffset.MinValue;
        public string Summary { get; set; } = "";
    }
}
