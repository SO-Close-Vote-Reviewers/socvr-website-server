using SOCVR.Website.Server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    /// <summary>
    /// Used to translate a path from the url into a physical location on the disk.
    /// </summary>
    public interface IContentFilePathTranslator
    {
        /// <summary>
        /// Turns the provided url path into a file system path.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        PhysicalFilePath TranslatePath(ContentFilePath path, ContentFilePathType type);
    }

    public enum ContentFilePathType
    {
        MarkdownFile,
        NavigationDataFile,
        StylesFile
    }
}
