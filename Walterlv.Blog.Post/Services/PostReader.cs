using Jurassic;
using Markdig;
using Pek.Markdig.HighlightJs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Walterlv.Blog.Services
{
    public static class PostReader
    {
        public static (YamlFrontMeta? metadata, string? summary, string? content) ReadFromFile(FileInfo file)
        {
            var (metadataPart, summaryPart, postPart) = SpanFromFile(file);

            if (string.IsNullOrWhiteSpace(metadataPart))
            {
                return (null, null, null);
            }

            var metadata = new Deserializer().Deserialize<YamlFrontMeta>(metadataPart);
            var summary = Markdown.ToPlainText(summaryPart ?? "").Trim();

            var content = Markdown.ToHtml(postPart ?? "", new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build());
            content = content.Replace("<pre>", @"<pre class=""prettyprint"">");
            return (metadata, summary, content);
        }

        public static (string? metadataPart, string? summary, string? postPart) SpanFromFile(FileInfo file)
        {
            bool? containsYamlMatter = null;
            var inInPostPart = false;
            var afterSummary = false;
            var metadataLines = new List<string>();
            var summaryLines = new List<string>();
            var postLines = new List<string>();

            using (var fileStream = file.OpenRead())
            using (var reader = new StreamReader(fileStream, Encoding.UTF8, true))
            {
                var line = reader.ReadLine()?.Trim('\r', '\n');
                while (line != null)
                {
                    if (containsYamlMatter == null)
                    {
                        if (line == "---")
                        {
                            containsYamlMatter = true;
                            line = reader.ReadLine()?.Trim('\r', '\n');
                            continue;
                        }

                        containsYamlMatter = false;
                        break;
                    }

                    if (inInPostPart)
                    {
                        postLines.Add(line);
                        if (!afterSummary)
                        {
                            if (line == "---")
                            {
                                afterSummary = true;
                            }
                            else
                            {
                                summaryLines.Add(line);
                            }
                        }
                    }
                    else if (line != "---")
                    {
                        metadataLines.Add(line.Trim());
                    }
                    else
                    {
                        inInPostPart = true;
                        postLines.Add(line);
                    }

                    line = reader.ReadLine()?.Trim('\r', '\n');
                }
            }

            var metadataPart = string.Join('\n', metadataLines);
            var summaryPart = string.Join('\n', summaryLines);
            var postPart = string.Join('\n', postLines);
            return (metadataPart, summaryPart, postPart);
        }
    }
}
