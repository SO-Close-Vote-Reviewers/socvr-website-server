using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class ContentFilePathTranslator : IContentFilePathTranslator
    {
        private readonly Configuration config;

        public ContentFilePathTranslator(IOptions<Configuration> configOptions)
        {
            config = configOptions.Value;
        }

        public string TranslatePath(string inputPath, ContentFilePathType type)
        {
            switch (type)
            {
                case ContentFilePathType.MarkdownFile:
                    return TranslatePath_MarkdownFile(inputPath);
                case ContentFilePathType.NavigationDataFile:
                    return TranslatePath_NavigationDataFile(inputPath);
                case ContentFilePathType.StylesFile:
                    return TranslatePath_StylesFile(inputPath);
                default:
                    throw new ArgumentException("Unknown content file path type");
            }
        }

        private string TranslatePath_MarkdownFile(string inputPath)
        {
            var inputPathWithExtension = string.IsNullOrWhiteSpace(inputPath)
                ? config.DefaultMarkdownFileName
                : inputPath + ".md";

            // input paths will only contain "/", not "\".
            inputPathWithExtension = inputPathWithExtension.Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(config.UserContentFolderPath, config.ContentPageFilesFolder, inputPathWithExtension);

            return fullPath;
        }

        private string TranslatePath_NavigationDataFile(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                inputPath = config.DefaultMarkdownFileName;

            var pathSections = inputPath.Split('/');
            pathSections[pathSections.Length - 1] = config.NavigationDataFileName;

            var fixedInputPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathSections);

            var fullPath = Path.Combine(config.UserContentFolderPath, config.ContentPageFilesFolder, fixedInputPath);
            return fullPath;
        }

        private string TranslatePath_StylesFile(string inputPath)
        {
            var fixedInputPath = inputPath.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(config.UserContentFolderPath, config.StyleFilesFolder, fixedInputPath);
            return fullPath;
        }
    }
}
