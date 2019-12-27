using dotnetCampus.Cli;

namespace Walterlv.Blog.Data
{
    internal class Options
    {
        [Option("StaticHost")]
        public string StaticHost { get; set; } = "";
    }
}