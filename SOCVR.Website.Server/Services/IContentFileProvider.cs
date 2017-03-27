using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IContentFileProvider
    {
        bool TryGetContentFileContents(ContentFilePath path, ContentFilePathType type, out string fileContents);
    }
}
