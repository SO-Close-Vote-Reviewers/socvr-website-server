using SOCVR.Website.Server.Models;
using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class NavigationDataFileProviderTests
    {
        [Fact]
        public void ReadNavigationFile_SingleLine() => ReadNavigationFile(
            CreateNavFileContents(NavFileLineType.Link),
            new NavLink[]
            {
                new NavLink { Display = "Display-1", Path = "Link-1" },
            });

        [Fact]
        public void ReadNavigationFile_EmptyFile() => ReadNavigationFile("", new NavLink[] { });

        [Fact]
        public void ReadNavigationFile_AllLineTypes() => ReadNavigationFile(
            CreateNavFileContents(NavFileLineType.Empty, NavFileLineType.Commented, NavFileLineType.Link, NavFileLineType.Commented, NavFileLineType.Empty),
            new NavLink[] 
            {
                new NavLink { Display = "Display-3", Path = "Link-3" },
            });

        private void ReadNavigationFile(string fileContents, params NavLink[] expectedNavLinks)
        {
            var fileName = "my-file";

            var translator = new MockContentFilePathTranslator();
            var fileProvider = new MockFileProvider();
            fileProvider.RegisterFile(fileName, fileContents);

            var navFileProvider = new NavigationDataFileProvider(translator, fileProvider);

            var actualNavLinks = navFileProvider.ReadNavigationFile(fileName);
            Assert.True(expectedNavLinks.SequenceEqual(actualNavLinks));
        }

        private string CreateNavFileContents(params NavFileLineType[] lineTypes)
        {
            var builder = new StringBuilder();

            foreach (var type in lineTypes.Select((x, i) => new { Value = x, LineNumber = i + 1 }))
            {
                switch (type.Value)
                {
                    case NavFileLineType.Commented:
                        builder.AppendLine($"# Commented line {type.LineNumber}");
                        break;
                    case NavFileLineType.Empty:
                        builder.AppendLine();
                        break;
                    case NavFileLineType.Link:
                        builder.AppendLine($"Display-{type.LineNumber},Link-{type.LineNumber}");
                        break;
                }
            }

            return builder.ToString();
        }

        private enum NavFileLineType
        {
            Empty,
            Commented,
            Link
        }
    }
}
