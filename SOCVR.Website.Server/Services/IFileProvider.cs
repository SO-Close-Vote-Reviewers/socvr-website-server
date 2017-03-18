using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IFileProvider
    {
        bool DoesFileExist(string path);
        string GetFileText(string path);
        string[] GetFileLines(string path);
    }
}
