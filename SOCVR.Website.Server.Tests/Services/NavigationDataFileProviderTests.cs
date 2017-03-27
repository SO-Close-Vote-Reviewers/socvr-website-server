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
        public void ReadNavigationFile_SingleLine()
        {
            var translator = new MockContentFilePathTranslator();
            var fileProvider = new MockFileProvider();

            var navFileProvider = new NavigationDataFileProvider(translator, fileProvider);

            fileProvider.RegisterFile("my-path", "DisplayName,LinkPath");

            var actual = navFileProvider.ReadNavigationFile("my-path");
            var expected = new[] { new NavLink { Display = "DisplayName", Path = "LinkPath" } };

            Assert.True(expected.SequenceEqual(actual));
        }

        public void ReadNavigationFile_EmptyLines()
        {

        }

        public void ReadNavigationFile_CommentedLines()
        {

        }

        public void ReadNavigationFile_AllLineTypes()
        {

        }
    }
}
