using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockContentFilePathTranslator : IContentFilePathTranslator
    {
        public PhysicalFilePath TranslatePath(ContentFilePath path, ContentFilePathType type)
        {
            return path.Value;
        }
    }
}
