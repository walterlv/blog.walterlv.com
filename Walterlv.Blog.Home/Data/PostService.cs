using System;
using System.Linq;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class PostService
    {
        public Task<PostBrief[]> GetPostBriefListAsync(int page = 0)
        {
            return Task.FromResult(new PostBrief[0]);
        }
    }
}
