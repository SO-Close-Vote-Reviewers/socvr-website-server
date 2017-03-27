using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SOCVR.Website.Server.DataTypes;
using System.Linq;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockContentFilePathSplitter : IContentFilePathSplitter
    {
        private int pathsToMake;

        public MockContentFilePathSplitter(int numberOfPathsToCreate)
        {
            pathsToMake = numberOfPathsToCreate;
        }

        public IEnumerable<ContentFilePath> SplitContentFilePath(ContentFilePath path)
        {
            return Enumerable.Range(1, pathsToMake)
                .Select(x => new ContentFilePath($"file-{x}"));
        }
    }
}
