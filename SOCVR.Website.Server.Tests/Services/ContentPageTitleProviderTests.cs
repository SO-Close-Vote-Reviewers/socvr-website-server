using SOCVR.Website.Server.DataTypes;
using SOCVR.Website.Server.Models;
using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class ContentPageTitleProviderTests
    {
        [Fact]
        public void GetContentPageTitle_RootItem() => GetContentPageTitle("Path-1", "Display-1",
            new NavLink { Display = "Display-1", Path = "Path-1" },
            new NavLink { Display = "Display-2", Path = "Path-2" });

        [Fact]
        public void GetContentPageTitle_Index() => GetContentPageTitle(null, "The Default File",
            new NavLink { Display = "The Default File", Path = "/" });

        [Fact]
        public void GetContentPageTitle_OneSubfolder() => GetContentPageTitle("path-1/path-2", "My Second Page",
            new NavLink { Display = "My Second Page", Path = "/path-1/path-2" });

        private void GetContentPageTitle(ContentFilePath input, string expected, params NavLink[] navLinkEntries)
        {
            var translator = new MockContentFilePathTranslator();
            var navFileProvider = new MockNavigationDataFileProvider(navLinkEntries);
            var titleProvider = new ContentPageTitleProvider(navFileProvider, translator);

            var actual = titleProvider.GetContentPageTitle(input);
            Assert.Equal(expected, actual);
        }
    }
}
