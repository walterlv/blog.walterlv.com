using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Walterlv.Blog.Controllers
{
    [ApiController]
    [Route("static/posts")]
    public class PostImageController : ControllerBase
    {
        [HttpGet("{imageId}")]
        public FileResult? Get(string imageId)
        {
            var path = Path.Combine(@"D:\Services\walterlv.github.io\static\posts", imageId);
            if (System.IO.File.Exists(path))
            {
                var content = System.IO.File.ReadAllBytes(path);
                return File(content, "image/png");
            }
            else
            {
                return null;
            }
        }
    }
}
