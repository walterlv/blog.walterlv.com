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
        public IEnumerable<PostBrief> Get()
        {
            return _postGenerator.GetAll();
        }


        [HttpGet("page{pageIndex}")]
        public IEnumerable<PostBrief> Get(int pageIndex)
        {
            return _postGenerator.GetAll().Skip(pageIndex * 30).Take(30);
        }
    }
}
