using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class ContentFileProviderTests
    {
        [Fact]
        public void TryGetContentFileContents_FileExists()
        {
            var config = new MockConfigurationOptions();
            var fileProvider = new MockFileProvider();
            var translator = new MockContentFilePathTranslator();

            var fileName = "my-file";
            var expectedFileContents = "some contents";
            fileProvider.RegisterFile(fileName, expectedFileContents);

            var contentFileProvider = new ContentFileProvider(config, fileProvider, translator);

            var existsActual = contentFileProvider.TryGetContentFileContents(fileName, ContentFilePathType.MarkdownFile, out string fileContentsActual);

            Assert.Equal(true, existsActual);
            Assert.Equal(expectedFileContents, fileContentsActual);
        }

        [Fact]
        public void TryGetContentFileContents_FileDoesNotExist()
        {
            var config = new MockConfigurationOptions();
            var fileProvider = new MockFileProvider();
            var translator = new MockContentFilePathTranslator();

            var contentFileProvider = new ContentFileProvider(config, fileProvider, translator);

            var existsActual = contentFileProvider.TryGetContentFileContents("my-file", ContentFilePathType.MarkdownFile, out string fileContentsActual);

            Assert.Equal(false, existsActual);
            Assert.Equal(null, fileContentsActual);
        }
    }
}
