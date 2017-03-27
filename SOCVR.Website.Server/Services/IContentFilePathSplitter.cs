using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IContentFilePathSplitter
    {
        IEnumerable<ContentFilePath> SplitContentFilePath(ContentFilePath path);
    }
}
