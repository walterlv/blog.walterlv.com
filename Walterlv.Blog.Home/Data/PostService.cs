using System;
using System.Linq;
using System.Threading.Tasks;

namespace Walterlv.Blog.Data
{
    public class PostService
    {
        private static readonly PostBrief[] SamplePostBriefList = new[]
        {
            new PostBrief("match-web-url-using-regex", "ʹ��������ʽ������׼ȷƥ������/��ַ")
            {
                UpdateTime = "2019-12-09 08:58",
                Summary = @"�������Ҫ׼ȷ��֪��һ���ַ����Ƿ�������/��ַ/URL����Ȼ����ʹ�� .��/ ��Щ��ģ��ƥ�䣬����������С�
ʵ���ϵ���ʹ��������ʽ����ȷƥ��Ҳ�Ƿǳ����ӵģ�ͨ���������жϻ�򵥺ܶࡣ����������Ȼ�������Ķ��������������ƥ��һ���ַ����Ƿ�������������ַ����Ҫ����ô�ߵĳ��ϣ�ʹ�ñ��ĵ�������ʽд�Ĵ����Ƚϼ򵥡�",
            },
            new PostBrief("csharp-nullable-analysis-attributes", "C# 8.0 �Ŀɿ��������ͣ���ֹ�ǼӸ��ʺ�Ŷ���㻹�кܶ��ֲ�ͬ�Ŀɿ��淨")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"C# 8.0 �����˿ɿ��������ͣ������ͨ�� ? Ϊ�ֶΡ����ԡ���������������ֵ������Ƿ��Ϊ null �����ԡ�
�������������ڰ���ԭ�еľ���ĿǨ�Ƶ��ɿ����͵�ʱ����ͻᷢ�����Զ���������и��ӣ���Ϊ��д�Ĵ������ֻ�ڲ�������¿ɿգ���������²��ɿգ����ߴ����ʱ�ſ�Ϊ�գ�����ǿ�ʱ�򲻿�Ϊ�ա�",
            },
            new PostBrief("get-derived-type-name-without-base-type-name", "ʹ��������ʽ������׼ȷƥ������/��ַ")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"������ MenuItem�������� WalterlvMenuItem��FooMenuItem�������� Configuration�������� WalterlvConfiguration��ExtensionConfiguration���ڴ����У����ǿ��ܻ�Ϊ���ܹ�һ�ۿ�����֮��ļ̳У���������ϵ�����������ƺ�׺�д��ϻ�������ơ�����������������µĻ��಻����ʵ�ʵ�ҵ�����Զ��⣨�ļ�/���磩������ͨ������Ҫ���������׺��
�����ṩһ���򵥵ķ������������л���ĺ�׺ɾ����ֻȡ��ǰ����ǲ��֡�",
            },
            new PostBrief("windows-default-font-family", "ʹ��������ʽ������׼ȷƥ������/��ַ")
            {
                UpdateTime = "2019-12-08 07:29",
                Summary = @"��Ϊ����Ӧ�õĿ����ߣ����Ƕ�����Ϊϵͳ��Ĭ�������ǡ�΢���źڡ���Ȼ�������Ĳ�����������⣬��������ڿ������ػ�Ӧ�õ�ʱ��ȿӡ�
���Ǳ��Ĵ����˽� Windows ϵͳ��Ĭ�����塣",
            },
        };

        public Task<PostBrief[]> GetPostBriefListAsync(int page = 0)
        {
            return Task.FromResult(SamplePostBriefList);
        }
    }
}
