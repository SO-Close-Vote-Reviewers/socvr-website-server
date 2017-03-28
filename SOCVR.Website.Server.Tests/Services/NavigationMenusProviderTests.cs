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
    public class NavigationMenusProviderTests
    {
        [Fact]
        public void GetNavigationMenus_Root() => GetNavigationMenus(1);

        [Fact]
        public void GetNavigationMenus_OneSubfolder() => GetNavigationMenus(2);

        [Fact]
        public void GetNavigationMenus_TwoSubfolders() => GetNavigationMenus(3);

        private void GetNavigationMenus(int folderLevels)
        {
            var navFileProvider = new MockNavigationDataFileProvider(3);
            var splitter = new MockContentFilePathSplitter(folderLevels);
            var fileProvider = new MockFileProvider();

            var currentLevelFile = "";

            foreach (var folderLevel in Enumerable.Range(1, folderLevels))
            {
                var thisFile = $"file-{folderLevel}";

                if (!string.IsNullOrWhiteSpace(currentLevelFile))
                {
                    thisFile = $"{currentLevelFile}/{thisFile}";
                }

                fileProvider.RegisterFile(thisFile, "Display,Link");
            }

            var navMenuProvider = new NavigationMenusProvider(navFileProvider, splitter);
            var actualNavCount = navMenuProvider.GetNavigationMenus(currentLevelFile).Count();

            Assert.Equal(folderLevels, actualNavCount);
        }
    }
}
