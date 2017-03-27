using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockContentFilePathSplitter : IContentFilePathSplitter
    {
        public IEnumerable<ContentFilePath> SplitContentFilePath(ContentFilePath path)
        {
            throw new NotImplementedException();
        }
    }
}
