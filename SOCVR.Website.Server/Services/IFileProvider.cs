using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IFileProvider
    {
        bool DoesFileExist(PhysicalFilePath path);
        string GetFileText(PhysicalFilePath path);
        string[] GetFileLines(PhysicalFilePath path);
        byte[] GetFileBytes(PhysicalFilePath path);
    }
}
