using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Walterlv.Blog.Services;

namespace Walterlv.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly PostGenerator _postGenerator;

        public PostController(ILogger<BlogController> logger, PostGenerator postGenerator)
        {
            _logger = logger;
            _postGenerator = postGenerator;
        }

        [HttpGet("{pageId}")]
        public ActionResult<Post> Get(string pageId)
        {
            if (pageId.EndsWith(".html"))
            {
                pageId = pageId.Substring(0, pageId.Length - 5);
            }
            var post = _postGenerator.Get(pageId);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                return post;
            }
        }
    }
}
