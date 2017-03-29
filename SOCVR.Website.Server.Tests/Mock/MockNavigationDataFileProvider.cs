using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;
using SOCVR.Website.Server.Models;
using System.Linq;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockNavigationDataFileProvider : INavigationDataFileProvider
    {
        private List<NavLink> entriesInFile;

        public MockNavigationDataFileProvider(int records)
        {
            entriesInFile = Enumerable.Range(1, records)
                .Select(x => new NavLink
                {
                    Display = $"Display-{x}",
                    Path = $"Path-{x}"
                })
                .ToList();
        }

        public MockNavigationDataFileProvider(params NavLink[] entries)
        {
            entriesInFile = entries.ToList();
        }

        public IEnumerable<NavLink> ReadNavigationFile(ContentFilePath path)
        {
            return entriesInFile;
        }
    }
}
