using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockDirectoryProvider : IDirectoryProvider
    {
        public string LastCreatedDirectoryPath { get; private set; }
        private bool directoryLookupExists;

        public MockDirectoryProvider(bool dirExists)
        {
            directoryLookupExists = dirExists;
        }

        public void CreateDirectory(PhysicalFilePath path)
        {
            LastCreatedDirectoryPath = path;
        }

        public bool DoesDirectoryExist(PhysicalFilePath path)
        {
            return directoryLookupExists;
        }
    }
}
