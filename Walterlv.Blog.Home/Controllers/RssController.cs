using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Walterlv.Blog.Data;

namespace Walterlv.Blog.Controllers
{
    [ApiController]
    [Route("feed.xml")]
    [Route("feed")]
    [Route("rss.xml")]
    [Route("rss")]
    public class RssController : Controller
    {
        private readonly PostGenerator _postGenerator;

        public RssController(PostGenerator postGenerator)
        {
            _postGenerator = postGenerator;
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 5)]
        public IActionResult Get()
        {
            var posts = _postGenerator.GetAll(15);
            var rss = new SyndicationFeed(
                "吕毅 (walterlv) 博客",
                ".NET/UWP/WPF/Roslyn/Windows 开发者，微软最有价值专家，Microsoft MVP",
                new Uri("https://blog.walterlv.com"),
                "blog.walterlv.com",
                posts[0].UpdateTime)
            {
                Copyright = new TextSyndicationContent($"© 2014-{DateTimeOffset.UtcNow.ToLocalTime().Year} walterlv, all rights reserved."),
                Language = "zh-cn",
                ImageUrl = new Uri("https://s.blog.walterlv.com/img/logo.png"),
                Items = posts.Select(post =>
                {
                    var item = new SyndicationItem(
                        post.Title,
                        post.Content,
                        new Uri($"https://blog.walterlv.com/post/{post.Id}"),
                        post.Id,
                        post.UpdateTime)
                    {
                        PublishDate = post.PublishTime,
                    };
                    item.Authors.Add(new SyndicationPerson("walterlv", "walterlv", "https://blog.walterlv.com/about"));
                    return item;
                }),
            };

            using var stream = new MemoryStream();
            using (var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
            }))
            {
                var rssFormatter = new Rss20FeedFormatter(rss, false);
                rssFormatter.WriteTo(xmlWriter);
                xmlWriter.Flush();
            }
            return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
        }
    }
}
