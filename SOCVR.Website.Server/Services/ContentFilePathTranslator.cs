using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Services
{
    public class ContentFilePathTranslator : IContentFilePathTranslator
    {
        private readonly Configuration config;

        public ContentFilePathTranslator(IOptions<Configuration> configOptions)
        {
            config = configOptions.Value;
        }

        public PhysicalFilePath TranslatePath(ContentFilePath path, ContentFilePathType type)
        {
            switch (type)
            {
                case ContentFilePathType.MarkdownFile:
                    return TranslatePath_MarkdownFile(path);
                case ContentFilePathType.NavigationDataFile:
                    return TranslatePath_NavigationDataFile(path);
                case ContentFilePathType.StylesFile:
                    return TranslatePath_StylesFile(path);
                case ContentFilePathType.ImageFile:
                    return TranslatePath_ImageFile(path);
                default:
                    throw new ArgumentException("Unknown content file path type");
            }
        }

        private string TranslatePath_MarkdownFile(ContentFilePath path)
        {
            var inputPathWithExtension = string.IsNullOrWhiteSpace(path)
                ? config.DefaultMarkdownFileName
                : path + ".md";

            // input paths will only contain "/", not "\".
            inputPathWithExtension = inputPathWithExtension.Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(config.CloneDir, config.ContentPageFilesFolder, inputPathWithExtension);

            return fullPath;
        }

        private string TranslatePath_NavigationDataFile(ContentFilePath path)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = config.DefaultMarkdownFileName;

            var pathSections = path.Value.Split('/');
            pathSections[pathSections.Length - 1] = config.NavigationDataFileName;

            var fixedInputPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathSections);

            var fullPath = Path.Combine(config.CloneDir, config.ContentPageFilesFolder, fixedInputPath);
            return fullPath;
        }

        private string TranslatePath_StylesFile(string inputPath)
        {
            var fixedInputPath = inputPath.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(config.CloneDir, config.StyleFilesFolder, fixedInputPath);
            return fullPath;
        }

        private string TranslatePath_ImageFile(string inputPath)
        {
            var fixedInputPath = inputPath.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(config.CloneDir, "images", fixedInputPath);
            return fullPath;
        }
    }
}
