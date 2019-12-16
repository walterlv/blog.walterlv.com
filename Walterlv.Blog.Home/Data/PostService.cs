using System;
using System.Linq;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class PostService
    {
        private static readonly PostBrief[] SamplePostBriefList = new[]
        {
            new PostBrief("match-web-url-using-regex", "使用正则表达式尽可能准确匹配域名/网址")
            {
                UpdateTime = "2019-12-09 08:58",
                Summary = @"你可能需要准确地知道一段字符串是否是域名/网址/URL。虽然可以使用 .、/ 这些来模糊匹配，但会造成误判。
实际上单纯使用正则表达式来精确匹配也是非常复杂的，通过代码来判断会简单很多。不过本文依然从域名的定义出发来尽可能匹配一段字符串是否是域名或者网址，在要求不怎么高的场合，使用本文的正则表达式写的代码会比较简单。",
            },
            new PostBrief("csharp-nullable-analysis-attributes", "C# 8.0 的可空引用类型，不止是加个问号哦！你还有很多种不同的可空玩法")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"C# 8.0 引入了可空引用类型，你可以通过 ? 为字段、属性、方法参数、返回值等添加是否可为 null 的特性。
但是如果你真的在把你原有的旧项目迁移到可空类型的时候，你就会发现情况远比你想象当中复杂，因为你写的代码可能只在部分情况下可空，部分情况下不可空；或者传入空时才可为空，传入非空时则不可为空。",
            },
            new PostBrief("get-derived-type-name-without-base-type-name", "使用正则表达式尽可能准确匹配域名/网址")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"基类是 MenuItem，子类是 WalterlvMenuItem、FooMenuItem。基类是 Configuration，子类是 WalterlvConfiguration、ExtensionConfiguration。在代码中，我们可能会为了能够一眼看清类之间的继承（从属）关系而在子类名称后缀中带上基类的名称。但是由于这种情况下的基类不参与实际的业务，所以对外（文件/网络）的名称通常不需要带上这个后缀。
本文提供一个简单的方法，让子类中基类的后缀删掉，只取得前面的那部分。",
            },
            new PostBrief("windows-default-font-family", "使用正则表达式尽可能准确匹配域名/网址")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"作为中文应用的开发者，我们多半会认为系统的默认字体是“微软雅黑”。然而如果真的产生了这种误解，则很容易在开发本地化应用的时候踩坑。
于是本文带你了解 Windows 系统的默认字体。",
            },
        };

        public Task<PostBrief[]> GetPostBriefListAsync(int page = 0)
        {
            return Task.FromResult(SamplePostBriefList);
        }
    }
}
