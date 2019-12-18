using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Walterlv.Blog.Services
{
    public static class PostMetadata
    {
        public static (YamlFrontMeta? metadata, string? postData) ExtractFromFile(FileInfo file)
        {
            var (metadataPart, postPart) = SpanFromFile(file);

            if (string.IsNullOrWhiteSpace(metadataPart))
            {
                return (null, null);
            }

            var deserializer = new Deserializer();
            var metadata = deserializer.Deserialize<YamlFrontMeta>(metadataPart);

            return (metadata, postPart);
        }

        public static (string? metadataPart, string? postPart) SpanFromFile(FileInfo file)
        {
            bool? containsYamlMatter = null;
            var inInPostPart = false;
            var metadataLines = new List<string>();
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
                            line = reader.ReadLine()?.Trim();
                            continue;
                        }

                        containsYamlMatter = false;
                        break;
                    }

                    if (inInPostPart)
                    {
                        postLines.Add(line);
                    }
                    else if (line != "---")
                    {
                        metadataLines.Add(line);
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
            var postPart = string.Join('\n', postLines);
            return (metadataPart, postPart);
        }
    }
}
