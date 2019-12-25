using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Walterlv.BlogBanners.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RedirectController : ControllerBase
    {
        [HttpGet, Route("csdn/column.png")]
        public IActionResult UrlMove()
        {
            return Redirect("https://www.zhipin.com/job_detail/26bf2e69fc65103d1HN539S_FFQ~.html");
        }

        [HttpGet, Route("blog/header.png")]
        public IActionResult BulletinUrlMove()
        {
            return Redirect("https://blog.walterlv.com");
        }

        [HttpGet, Route("blog/footer.png")]
        public IActionResult BannerUrlMove()
        {
            return Redirect("https://www.zhipin.com/job_detail/26bf2e69fc65103d1HN539S_FFQ~.html");
        }
    }
}
