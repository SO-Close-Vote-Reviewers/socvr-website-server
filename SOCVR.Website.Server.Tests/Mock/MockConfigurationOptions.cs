using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    internal class MockConfigurationOptions : IOptions<Configuration>
    {
        public const string CloneDir = "git-dir";
        public const string ContentFilesFolder = "pages";
        public const string StyleFilesFolder = "styles";
        public const string DefaultMarkdownFile = "index.md";
        public const string NavigationDataFileName = "_nav.csv";

        public Configuration Value => new Configuration
        {
            ContentPageFilesFolder = ContentFilesFolder,
            DefaultMarkdownFileName = DefaultMarkdownFile,
            StyleFilesFolder = StyleFilesFolder,
            NavigationDataFileName = NavigationDataFileName,
            CloneDir = CloneDir,
            GitBranch = "master"
        };
    }
}
