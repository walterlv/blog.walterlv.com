﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Walterlv.BlogPartial.Data;
using static Walterlv.BlogPartial.Data.Site;

namespace Walterlv.BlogPartial.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMemoryCache _cache;
        private readonly VisitingInfoContext _context;

        public ImageController(IHttpContextAccessor accessor, IMemoryCache memoryCache, VisitingInfoContext context)
        {
            _accessor = accessor;
            _cache = memoryCache;
            _context = context;
        }

        [HttpGet, Route("csdn/column.png")]
        public FileResult GetImageToCsdn()
        {
            RecordVisitingInfo(Csdn, "/csdn/column.png");
            return GetImage("CSDN", "csdn-column");
        }

        [HttpGet, Route("blog/header.png")]
        public FileResult GetImageToBlog()
        {
            RecordVisitingInfo(Blog, "/blog/header.png");
            return GetImage("blog.walterlv.com", "blog-header");
        }

        [HttpGet, Route("blog/footer.png")]
        public FileResult GetBannerToBlog()
        {
            RecordVisitingInfo(Blog, "/blog/footer.png");
            return GetImage("blog.walterlv.com", "blog-footer");
        }

        private FileResult GetImage(string from, string name)
        {
            var file = _cache.GetOrCreate(name, entry => System.IO.File.ReadAllBytes($"{name}.png"));
            return File(file, "image/png");
        }

        private void RecordVisitingInfo(string site, string url)
        {
            // 获取用户的真实 IP（此字段记录了出发点 IP 和代理服务器经过的 IP）。
            _accessor.HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var ip);

            // 获取用户浏览器信息。
            _accessor.HttpContext.Request.Headers.TryGetValue("User-Agent", out var userAgent);

            _context.VisitingInfoSet.Add(new VisitingInfo
            {
                Time = DateTimeOffset.Now,
                Ip = ip,
                UserAgent = userAgent,
                Site = site,
                Url = url,
            });
            _context.SaveChangesAsync();

            // 输出摘要。
            var csdnCount = _context.VisitingInfoSet.Where(x => x.Site == Csdn).Count();
            var blogCount = _context.VisitingInfoSet.Where(x => x.Site == Blog).Count();
            Console.ForegroundColor = SiteColorMapping[site];
            Console.WriteLine($@"[{DateTime.Now}] [{site}{url}] {ip}                ");
            Console.WriteLine($@"CSDN = {csdnCount} | Blog = {blogCount}");
            Console.ResetColor();
        }

        private static readonly Dictionary<string, ConsoleColor> SiteColorMapping
            = new Dictionary<string, ConsoleColor>
            {
                { Csdn, ConsoleColor.DarkYellow },
                { Blog, ConsoleColor.DarkCyan },
            };
    }
}
