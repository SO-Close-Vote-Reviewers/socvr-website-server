using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class ContentPageProviderTests
    {
        [Fact]
        public void TryGetContentPageContents_IndexIfNull()
        {
            var config = new MockConfigurationOptions();
            var fileProvider = new MockFileProvider(true);
            var translator = new MockContentFilePathTranslator();
            var contentFileProvider = new ContentFileProvider(config, fileProvider, translator);

            var expectedSuccess = true;
            var expectedContents = MockFileProvider.FileContents;
            var expectedPath = Path.Combine(MockConfigurationOptions.ContentPath, "pages", "index.md");
            var actualSuccess = contentFileProvider.TryGetContentFileContents(null, ContentFilePathType.MarkdownFile, out string actualContents);

            Assert.Equal(expectedSuccess, actualSuccess);
            Assert.Equal(expectedContents, actualContents);
            Assert.Equal(expectedPath, fileProvider.GetFileTextParamValue);
        }
    }
}
