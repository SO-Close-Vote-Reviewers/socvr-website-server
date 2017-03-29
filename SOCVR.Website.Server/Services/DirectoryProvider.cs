using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;
using System.IO;

namespace SOCVR.Website.Server.Services
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public void CreateDirectory(PhysicalFilePath path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DoesDirectoryExist(PhysicalFilePath path)
        {
            return Directory.Exists(path);
        }
    }
}
