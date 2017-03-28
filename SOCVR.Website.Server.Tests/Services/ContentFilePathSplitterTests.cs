using SOCVR.Website.Server.DataTypes;
using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class ContentFilePathSplitterTests
    {
        [Fact]
        public void SplitContentFilePath_NullPath() => SplitContentFilePath(null, new[] { "" });

        [Fact]
        public void SplitContentFilePath_RootPath() => SplitContentFilePath("my-page", new[] { "my-page" });

        [Fact]
        public void SplitContentFilePath_OneSubdirectory() => SplitContentFilePath("level-1/level-2", new[] { "level-1", "level-1/level-2" });

        [Fact]
        public void SplitContentFilePath_TwoSubdirectories() => SplitContentFilePath("level-1/level-2/level-3", new[] { "level-1", "level-1/level-2", "level-1/level-2/level-3" });

        private void SplitContentFilePath(string path, params string[] expected)
        {
            var splitter = new ContentFilePathSplitter();
            ContentFilePath contentPath = path;

            var actual = splitter.SplitContentFilePath(contentPath)
                .Select(x => (string)x)
                .ToArray();

            Assert.Equal(expected, actual);
        }
    }
}
