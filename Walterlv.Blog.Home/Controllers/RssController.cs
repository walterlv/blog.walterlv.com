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
    [Route("feed.xml"), Route("feed"), Route("rss.xml"), Route("rss")]
    public class RssController : Controller
    {
        private readonly PostGenerator _postGenerator;
        private readonly PostStaticRedirector _staticRedirector;
        private readonly SiteAnalytics _analytics;

        public RssController(
            PostGenerator postGenerator,
            PostStaticRedirector staticRedirector,
            SiteAnalytics analytics)
        {
            _postGenerator = postGenerator;
            _staticRedirector = staticRedirector;
            _analytics = analytics;
        }

        [HttpGet, ResponseCache(Duration = 60 * 5)]
        public IActionResult Get()
        {
            _analytics.Handle("/feed.xml", "RSS 客户端正在更新订阅……");

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
                        CombinePostContentWithLicense(post.Id, post.Content),
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

        private string CombinePostContentWithLicense(string id, string postContent) => $@"{postContent}
<p>本文会经常更新，请阅读原文：<a href=""https://blog.walterlv.com/post/{id}"">https://blog.walterlv.com/post/{id}</a>，以避免陈旧错误知识的误导，同时有更好的阅读体验。</p>
<p>如果你想持续阅读我的最新博客，请点击 <a href=""https://blog.walterlv.com/feed.xml"">RSS 订阅</a>，或者<a href=""https://walterlv.blog.csdn.net/"">前往 CSDN 关注我的主页</a>。</p>
<p>
    <a rel=""知识共享许可协议"" href=""https://creativecommons.org/licenses/by-nc-sa/4.0/"">
        <img alt=""知识共享许可协议"" src=""{_staticRedirector.Host}/img/by-nc-sa.svg"" />
    </a>
    本作品采用
    <a rel=""license"" href=""https://creativecommons.org/licenses/by-nc-sa/4.0/"">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>
    进行许可。欢迎转载、使用、重新发布，但务必保留文章署名
    <strong>吕毅</strong>
    （包含链接：
    <a href=""https://blog.walterlv.com"">https://blog.walterlv.com</a>
    ），不得用于商业目的，基于本文修改后的作品务必以相同的许可发布。如有任何疑问，请
    <a href=""mailto:walter.lv@qq.com"">与我联系 (walter.lv@qq.com)</a>
    。
</p>";
    }
}
