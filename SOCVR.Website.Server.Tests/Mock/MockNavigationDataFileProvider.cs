using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;
using SOCVR.Website.Server.Models;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockNavigationDataFileProvider : INavigationDataFileProvider
    {
        public IEnumerable<NavLink> ReadNavigationFile(ContentFilePath path)
        {
            throw new NotImplementedException();
        }
    }
}
