using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class ContentFilePathTranslatorTests
    {
        [Fact]
        public void TranslatePath_MarkdownFile_Null() => TranslatePath_MarkdownFile(null, MockConfigurationOptions.DefaultMarkdownFile);

        [Fact]
        public void TranslatePath_MarkdownFile_RootLevel() => TranslatePath_MarkdownFile("my-cool-page", "my-cool-page.md");

        [Fact]
        public void TranslatePath_MarkdownFile_OneSubfolder() => TranslatePath_MarkdownFile("folder/my-page", "folder", "my-page.md");

        [Fact]
        public void TranslatePath_MarkdownFile_ManySubfolders() => TranslatePath_MarkdownFile("folder-a/folder-b/folder-c/the-page", "folder-a", "folder-b", "folder-c", "the-page.md");

        private void TranslatePath_MarkdownFile(string inputPath, params string[] expectedPathSuffixSections) => 
            TranslatePath(inputPath, MockConfigurationOptions.ContentFilesFolder, ContentFilePathType.MarkdownFile, expectedPathSuffixSections);

        [Fact]
        public void TranslatePath_NavDataFile_Null() => TranslatePath_NavDataFile(null, "_nav.csv");

        [Fact]
        public void TranslatePath_NavDataFile_RootLevel() => TranslatePath_NavDataFile("root-level-page", "_nav.csv");

        [Fact]
        public void TranslatePath_NavDataFile_OneSubfolder() => TranslatePath_NavDataFile("folder/my-page", "folder", "_nav.csv");

        [Fact]
        public void TranslatePath_NavDataFile_ManySubfolders() => TranslatePath_NavDataFile("folder-a/folder-b/folder-c/the-page", "folder-a", "folder-b", "folder-c", "_nav.csv");

        private void TranslatePath_NavDataFile(string inputPath, params string[] expectedPathSuffixSections) => 
            TranslatePath(inputPath, MockConfigurationOptions.ContentFilesFolder, ContentFilePathType.NavigationDataFile, expectedPathSuffixSections);

        [Fact]
        public void TranslatePath_StylesFile_RootLevel() => TranslatePath_StylesFile("my-styles.less", "my-styles.less");

        [Fact]
        public void TranslatePath_StylesFile_OneSubfolder() => TranslatePath_StylesFile("folder/styles.less", "folder", "styles.less");

        [Fact]
        public void TranslatePath_StylesFile_ManySubfolders() => TranslatePath_StylesFile("folder-a/folder-b/folder-c/styles.sass", "folder-a", "folder-b", "folder-c", "styles.sass");

        private void TranslatePath_StylesFile(string inputPath, params string[] expectedPathSuffixSections) =>
            TranslatePath(inputPath, MockConfigurationOptions.StyleFilesFolder, ContentFilePathType.StylesFile, expectedPathSuffixSections);

        private void TranslatePath(string inputPath, string repoRootFolder, ContentFilePathType type, params string[] expectedPathSuffixSections)
        {
            var expectedPathSections = new List<string>()
            {
                MockConfigurationOptions.ContentPath,
                repoRootFolder
            };

            expectedPathSections.AddRange(expectedPathSuffixSections);

            var expected = Path.Combine(expectedPathSections.ToArray());

            var config = new MockConfigurationOptions();
            var translator = new ContentFilePathTranslator(config);
            var actual = translator.TranslatePath(inputPath, type);

            Assert.Equal(expected, actual);
        }
    }
}
