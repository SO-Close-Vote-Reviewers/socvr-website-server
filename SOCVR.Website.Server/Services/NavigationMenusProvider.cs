using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.Models;
using Microsoft.Extensions.Options;
using System.IO;

namespace SOCVR.Website.Server.Services
{
    public class NavigationMenusProvider : INavigationMenusProvider
    {
        private readonly IFileProvider fileProvider;
        private readonly Configuration config;
        private readonly IContentFilePathTranslator translator;

        public NavigationMenusProvider(IFileProvider fileProviderService, IOptions<Configuration> configOptions, IContentFilePathTranslator translatorService)
        {
            fileProvider = fileProviderService;
            config = configOptions.Value;
            translator = translatorService;
        }

        public IEnumerable<IEnumerable<NavLink>> GetNavigationMenus(string path)
        {
            var pathSections = (path ?? "").Split('/');

            for (int i = 0; i < pathSections.Length; i++)
            {
                var sectionsForThisPath = pathSections.Where((x, index) => index <= i);
                var sectionPath = string.Join("/", sectionsForThisPath);
                yield return GetNavLinksForPath(sectionPath);
            }
        }

        private IEnumerable<NavLink> GetNavLinksForPath(string path)
        {
            var navCsvFilePath = translator.TranslatePath(path, ContentFilePathType.NavigationDataFile);
            var csvLines = fileProvider.GetFileLines(navCsvFilePath);

            var links = csvLines
                .Where(x => !x.StartsWith("#"))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split(','))
                .Select(x => new NavLink
                {
                    Display = x[0],
                    Path = x[1]
                });

            return links;
        }
    }
}
