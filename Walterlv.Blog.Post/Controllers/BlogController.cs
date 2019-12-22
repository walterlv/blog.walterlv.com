using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Walterlv.Blog.Data;
using Walterlv.Blog.Services;

namespace Walterlv.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly PostGenerator _postGenerator;

        public BlogController(ILogger<BlogController> logger, PostGenerator postGenerator)
        {
            _logger = logger;
            _postGenerator = postGenerator;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<PostBrief>> Get()
        {
            var list = _postGenerator.GetAll().ToList();
            return list;
        }


        [HttpGet("page{pageIndex}")]
        public ActionResult<IReadOnlyList<PostBrief>> Get(int pageIndex)
        {
            var list = _postGenerator.GetAll().Skip(pageIndex * 30).Take(30).ToList();
            return list.Count == 0 ? NotFound() : (ActionResult<IReadOnlyList<PostBrief>>)list;
        }
    }
}
