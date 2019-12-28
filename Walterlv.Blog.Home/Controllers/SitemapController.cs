using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleSiteMap;
using Walterlv.Blog.Data;

namespace Walterlv.Blog.Controllers
{
    [ApiController]
    [Route("sitemap.xml"), Route("sitemap")]
    public class SitemapController : Controller
    {
        private readonly PostGenerator _postGenerator;

        public SitemapController(PostGenerator postGenerator)
        {
            _postGenerator = postGenerator;
        }

        [HttpGet, ResponseCache(Duration = 60 * 5)]
        public ContentResult Get()
        {
            const string host = "https://blog.walterlv.com";

            var products = _postGenerator.GetAll();

            var postNodes = from p in products
                            select new SitemapNode(new Uri($"{host}/post/{p.Id}"), p.UpdateTime.ToLocalTime().DateTime, SitemapFrequency.Hourly, 1.0);
            var pageNodes = new SitemapNode[]
            {
                new SitemapNode(new Uri($"{host}/friends"), DateTime.Today),
                new SitemapNode(new Uri($"{host}/about"), DateTime.Today),
            };

            var sitemapService = new SitemapService();
            var xml = sitemapService.ConvertToXmlUrlset(postNodes.Concat(pageNodes).ToList());
            return Content(xml, "application/xml");
        }
    }
}
