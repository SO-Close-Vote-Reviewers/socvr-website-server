using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class FileProvider : IFileProvider
    {
        public bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }

        public string[] GetFileLines(string path)
        {
            throw new NotImplementedException();
        }

        public string GetFileText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
