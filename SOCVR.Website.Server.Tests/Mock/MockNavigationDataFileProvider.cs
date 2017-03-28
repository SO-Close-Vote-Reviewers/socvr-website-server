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
        private int recordsInFile;

        public MockNavigationDataFileProvider(int records)
        {
            recordsInFile = records;
        }

        public IEnumerable<NavLink> ReadNavigationFile(ContentFilePath path)
        {
            return Enumerable.Range(1, recordsInFile)
                .Select(x => new NavLink
                {
                    Display = $"Display-{x}",
                    Path = $"Path-{x}"
                });
        }
    }
}
