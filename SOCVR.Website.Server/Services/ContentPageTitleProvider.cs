using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;
using System.Text.RegularExpressions;

namespace SOCVR.Website.Server.Services
{
    public class ContentPageTitleProvider : IContentPageTitleProvider
    {
        private readonly INavigationDataFileProvider navFileProvider;
        private readonly IContentFilePathTranslator translator;

        public ContentPageTitleProvider(INavigationDataFileProvider navFileProviderService, IContentFilePathTranslator translatorService)
        {
            navFileProvider = navFileProviderService;
            translator = translatorService;
        }

        public string GetContentPageTitle(ContentFilePath path)
        {
            var contentFilePhysicalPath = translator.TranslatePath(path, ContentFilePathType.MarkdownFile);

            var navFile = navFileProvider.ReadNavigationFile(path);

            var navFileEntriesWithTranslatedPaths = navFile
                .Select(x =>
                {
                    var fixedPath = Regex.Replace(x.Path, @"^\/?(.*)$", "$1");
                    return new
                    {
                        x.Display,
                        PhysicalPath = (string)translator.TranslatePath(fixedPath, ContentFilePathType.MarkdownFile)
                    };
                });

            var matchingEntry = navFileEntriesWithTranslatedPaths
                .SingleOrDefault(x => x.PhysicalPath == contentFilePhysicalPath);

            return matchingEntry?.Display;
        }
    }
}
