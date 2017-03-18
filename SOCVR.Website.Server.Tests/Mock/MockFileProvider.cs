using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    internal class MockFileProvider : IFileProvider
    {
        private bool exists;

        public const string FileContents = "this is the file contents";
        public string DoesFileExistParamValue { get; private set; }
        public string GetFileTextParamValue { get; private set; }

        public MockFileProvider(bool doesExist)
        {
            exists = doesExist;
        }

        public bool DoesFileExist(string path)
        {
            DoesFileExistParamValue = path;
            return exists;
        }

        public string GetFileText(string path)
        {
            GetFileTextParamValue = path;
            return FileContents;
        }

        public string[] GetFileLines(string path)
        {
            throw new NotImplementedException();
        }
    }
}
