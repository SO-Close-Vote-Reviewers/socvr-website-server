using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.Models;
using Microsoft.Extensions.Options;
using System.IO;

namespace SOCVR.Website.Server.Services
{
    public class NavigationMenuesProvider : INavigationMenusProvider
    {
        private readonly IFileProvider fileProvider;
        private readonly Configuration config;

        public NavigationMenuesProvider(IFileProvider fileProviderService, IOptions<Configuration> configOptions)
        {
            fileProvider = fileProviderService;
            config = configOptions.Value;
        }

        public IEnumerable<IEnumerable<NavLink>> GetNavigationMenus(string path)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<NavLink> GetNavLinksForPath(string path)
        {
            var navCsvFilePath = Path.Combine(config.UserContentFolderPath, "pages", path, "_nav.csv");
            var csvLines = fileProvider.GetFileLines(navCsvFilePath);

            throw new NotImplementedException();
        }
    }
}
