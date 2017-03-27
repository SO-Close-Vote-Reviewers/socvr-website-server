using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;
using SOCVR.Website.Server.Models;

namespace SOCVR.Website.Server.Services
{
    public class NavigationDataFileProvider : INavigationDataFileProvider
    {
        private readonly IContentFilePathTranslator translator;
        private readonly IFileProvider fileProvider;

        public NavigationDataFileProvider(IContentFilePathTranslator translatorService, IFileProvider fileProviderService)
        {
            translator = translatorService;
            fileProvider = fileProviderService;
        }

        public IEnumerable<NavLink> ReadNavigationFile(ContentFilePath path)
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
