using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    internal class MockFileProvider : IFileProvider
    {
        public string DoesFileExistParamValue { get; private set; }
        public string GetFileTextParamValue { get; private set; }

        private Dictionary<string, string> registeredFiles;

        public MockFileProvider()
        {
            registeredFiles = new Dictionary<string, string>();
        }

        public bool DoesFileExist(string path)
        {
            DoesFileExistParamValue = path;
            return registeredFiles.ContainsKey(path);
        }

        public string GetFileText(string path)
        {
            GetFileTextParamValue = path;
            return registeredFiles[path];
        }

        public string[] GetFileLines(string path)
        {
            return registeredFiles[path].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public void RegisterFile(string path, string contents)
        {
            registeredFiles.Add(path, contents);
        }
    }
}
