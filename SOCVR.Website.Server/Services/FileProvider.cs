using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class FileProvider : IFileProvider
    {
        public bool DoesFileExist(PhysicalFilePath path)
        {
            return File.Exists(path);
        }

        public string[] GetFileLines(PhysicalFilePath path)
        {
            return File.ReadAllLines(path);
        }

        public string GetFileText(PhysicalFilePath path)
        {
            return File.ReadAllText(path);
        }
    }
}
