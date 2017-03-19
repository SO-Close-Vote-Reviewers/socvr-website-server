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
        public void GetNavigationMenus_Root()
        {
            var config = new MockConfigurationOptions();
            var fileProvider = new MockFileProvider();
            var translator = new MockContentFilePathTranslator();

            fileProvider.RegisterFile("my-page", MakeNavFileContents("Root", 2));

            var navMenuProvider = new NavigationMenusProvider(fileProvider, config, translator);

            var inputPath = "my-page";
            var actual = navMenuProvider.GetNavigationMenus(inputPath).ToList();

            Assert.Equal(1, actual?.Count);

            var actualRecord = actual.Single().ToList();

            Assert.Equal(2, actualRecord.Count);

            var expectedNavLinks = new List<NavLink>()
            {
                new NavLink{ Display = "Root-Display-1", Path = "Root-Link-1" },
                new NavLink{ Display = "Root-Display-2", Path = "Root-Link-2" }
            };

            Assert.True(expectedNavLinks.SequenceEqual(actualRecord));
        }

        [Fact]
        public void GetNavigationMenus_OneSubfolder()
        {
            var config = new MockConfigurationOptions();
            var fileProvider = new MockFileProvider();
            var translator = new MockContentFilePathTranslator();

            fileProvider.RegisterFile("level-1", MakeNavFileContents("Level-1", 3));
            fileProvider.RegisterFile("level-1/level-2", MakeNavFileContents("Level-2", 2));

            var navMenuProvider = new NavigationMenusProvider(fileProvider, config, translator);

            var inputPath = "level-1/level-2";
            var actualMenus = navMenuProvider.GetNavigationMenus(inputPath).ToList();

            Assert.Equal(2, actualMenus.Count);
            Assert.Equal(3, actualMenus[0].Count());
            Assert.Equal(2, actualMenus[1].Count());
        }

        private string MakeNavFileContents(string prefix, int numberOfRecords)
        {
            var numbers = Enumerable.Range(1, numberOfRecords);
            var entries = numbers.Select(x => $"{prefix}-Display-{x},{prefix}-Link-{x}");
            return string.Join(Environment.NewLine, entries);
        }
    }
}
