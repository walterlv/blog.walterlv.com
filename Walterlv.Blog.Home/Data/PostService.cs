using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class PostService
    {
        public Task<PostBrief[]> GetPostBriefListAsync(string page)
        {
            return Task.FromResult(new PostBrief[0]);
        }
    }
}
