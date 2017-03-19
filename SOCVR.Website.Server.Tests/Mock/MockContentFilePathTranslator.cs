using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockContentFilePathTranslator : IContentFilePathTranslator
    {
        public string TranslatePath(string inputPath, ContentFilePathType type)
        {
            return inputPath;
        }
    }
}
