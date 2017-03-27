using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Services
{
    public class ContentFileProvider : IContentFileProvider
    {
        private readonly Configuration config;
        private readonly IFileProvider fileProvider;
        private readonly IContentFilePathTranslator translator;

        public ContentFileProvider(IOptions<Configuration> configOptions, IFileProvider fileProviderService, IContentFilePathTranslator translatorService)
        {
            config = configOptions.Value;
            fileProvider = fileProviderService;
            translator = translatorService;
        }

        public bool TryGetContentFileContents(ContentFilePath path, ContentFilePathType type, out string fileContents)
        {
            fileContents = null;
            var physicalPath = translator.TranslatePath(path, type);

            var fileExists = fileProvider.DoesFileExist(physicalPath);

            if (fileExists)
                fileContents = fileProvider.GetFileText(physicalPath);

            return fileExists;
        }
    }
}
