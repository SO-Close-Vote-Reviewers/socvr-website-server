using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class ContentPageProvider : IContentPageProvider
    {
        private readonly Configuration config;
        private readonly IFileProvider fileProvider;

        public ContentPageProvider(IOptions<Configuration> configOptions, IFileProvider fileProviderService)
        {
            config = configOptions.Value;
            fileProvider = fileProviderService;
        }

        public bool TryGetContentPageContents(string path, out string contents)
        {
            contents = null;

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "index";
            }

            var expectedFilePath = Path.Combine(config.UserContentFolderPath, "pages", path + ".md");

            var exists = fileProvider.DoesFileExist(expectedFilePath);

            if (exists)
            {
                contents = fileProvider.GetFileText(expectedFilePath);
            }

            return exists;
        }
    }
}
