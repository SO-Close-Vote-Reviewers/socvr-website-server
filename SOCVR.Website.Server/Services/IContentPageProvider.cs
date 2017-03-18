using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IContentPageProvider
    {
        bool TryGetContentPageContents(string path, out string contents);
    }
}
