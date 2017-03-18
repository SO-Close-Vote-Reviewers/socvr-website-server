using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    internal class MockConfigurationOptions : IOptions<Configuration>
    {
        public const string ContentPath = "content-path";

        public Configuration Value => new Configuration
        {
            UserContentFolderPath = ContentPath
        };
    }
}
