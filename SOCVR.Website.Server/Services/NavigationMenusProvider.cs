using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.Models;
using Microsoft.Extensions.Options;
using System.IO;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Services
{
    public class NavigationMenusProvider : INavigationMenusProvider
    {
        private readonly INavigationDataFileProvider navFileProvider;
        private readonly IContentFilePathSplitter pathSplitter;

        public NavigationMenusProvider(INavigationDataFileProvider navFileProviderService, IContentFilePathSplitter pathSplitterService)
        {
            navFileProvider = navFileProviderService;
            pathSplitter = pathSplitterService;
        }

        public IEnumerable<IEnumerable<NavLink>> GetNavigationMenus(ContentFilePath path)
        {
            foreach (var sectionPath in pathSplitter.SplitContentFilePath(path))
            {
                yield return navFileProvider.ReadNavigationFile(sectionPath);
            }
        }
    }
}
