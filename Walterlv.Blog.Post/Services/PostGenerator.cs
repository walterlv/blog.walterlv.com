using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Walterlv.Blog.Data;

namespace Walterlv.Blog.Services
{
    public class PostGenerator
    {
        private Lazy<Dictionary<string, FileInfo>> _fileCache = new Lazy<Dictionary<string, FileInfo>>(GenerateFileCache, LazyThreadSafetyMode.PublicationOnly);
        private ConcurrentDictionary<string, Post?> _postCache = new ConcurrentDictionary<string, Post?>();

        public PostGenerator()
        {
            StartMonitorPosts();
        }

        public Post? Get(string id)
        {
            var value = _postCache.GetOrAdd(id, CreatePost);
            if (value is null)
            {
                _postCache.TryRemove(id, out _);
                return null;
            }
            return value;
        }

        public IReadOnlyList<PostBrief> GetAll()
        {
            return _postCache.Values.OfType<Post>().Select(x => new PostBrief(x)).ToList();
        }

        private Post? CreatePost(string id)
        {
            var fileCache = _fileCache.Value;
            if (fileCache.TryGetValue(id, out var path))
            {
                return CreatePostCore(id, path);
            }
            return null;
        }

        private Post? CreatePostCore(string id, FileInfo file)
        {
            var (metadata, summary, content) = PostReader.ReadFromFile(file);
            if (metadata != null && !string.IsNullOrWhiteSpace(content))
            {
                var (publishTime, updateTime) = ParsePublishAndUpdateTime(metadata.PublishDate, metadata.Date);
                var title = metadata.Title ?? id;
                return new Post(id, title, publishTime, updateTime, summary ?? "", content);
            }
            return null;
        }

        private (DateTimeOffset publishTime, DateTimeOffset updateTime) ParsePublishAndUpdateTime(string? publishTimeString, string? updateTimeString)
        {
            var hasPublishTime = DateTimeOffset.TryParse(publishTimeString, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var publishTime);
            var hasUpdateTime = DateTime.TryParse(updateTimeString, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var updateTime);

            if (hasPublishTime && hasUpdateTime)
            {
                // 博客被更新过。
                return (publishTime, updateTime);
            }
            else if (hasPublishTime)
            {
                // 博客被更新过，但格式不正确。
                return (publishTime, publishTime);
            }
            else if (hasUpdateTime)
            {
                // 博客仅发布过，没有更新，
                return (updateTime, updateTime);
            }
            else
            {
                // 这篇博客没有时间，通常说明这根本就不是一篇博客。
                var now = DateTimeOffset.Now;
                return (now, now);
            }
        }

        private async Task StartMonitorPosts()
        {
            while (true)
            {
                var fileCache = _fileCache.Value;
                _postCache = new ConcurrentDictionary<string, Post?>(fileCache.Select(x => new KeyValuePair<string, Post?>(x.Key, CreatePostCore(x.Key, x.Value))));

                await Task.Delay(5 * 60 * 1000);

                _fileCache = new Lazy<Dictionary<string, FileInfo>>(GenerateFileCache, LazyThreadSafetyMode.PublicationOnly);
            }
        }

        private static Dictionary<string, FileInfo> GenerateFileCache()
        {
            var idRegex = new Regex(@"(?<=\d{4}-\d{2}-\d{2}-).+(?=\.md)");
            var directory = new DirectoryInfo(@"D:\Services\blog.walterlv.com\_posts");
            var dictionary = from file in directory.EnumerateFiles("*.md", SearchOption.AllDirectories)
                             let match = idRegex.Match(file.Name)
                             where match.Success
                             select new KeyValuePair<string, FileInfo>(match.Value, file);
            return dictionary.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
