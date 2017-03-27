using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;

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
            var navFile = navFileProvider.ReadNavigationFile(path);

            var contentFilePhysicalPath = translator.TranslatePath(path, ContentFilePathType.MarkdownFile);

            throw new NotImplementedException();

        }
    }
}
