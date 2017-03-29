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
        private readonly IFileProvider fileProvider;
        private readonly IContentFilePathTranslator translator;

        public ContentFileProvider(IFileProvider fileProviderService, IContentFilePathTranslator translatorService)
        {
            fileProvider = fileProviderService;
            translator = translatorService;
        }

        public bool TryGetContentFileBytes(ContentFilePath path, ContentFilePathType type, out byte[] fileBytes)
        {
            return TryGetFile(path, type, out fileBytes, p => fileProvider.GetFileBytes(p));
        }

        public bool TryGetContentFileContents(ContentFilePath path, ContentFilePathType type, out string fileContents)
        {
            return TryGetFile(path, type, out fileContents, p => fileProvider.GetFileText(p));
        }

        private bool TryGetFile<T>(ContentFilePath path, ContentFilePathType type, out T result, Func<PhysicalFilePath, T> fileExtractionMethod)
        {
            result = default(T);

            var physicalPath = translator.TranslatePath(path, type);

            var fileExists = fileProvider.DoesFileExist(physicalPath);

            if (fileExists)
                result = fileExtractionMethod(physicalPath);

            return fileExists;
        }
    }
}
