using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IDirectoryProvider
    {
        bool DoesDirectoryExist(PhysicalFilePath path);

        void CreateDirectory(PhysicalFilePath path);
    }
}
