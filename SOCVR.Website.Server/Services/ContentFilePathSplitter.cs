using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOCVR.Website.Server.DataTypes;

namespace SOCVR.Website.Server.Services
{
    public class ContentFilePathSplitter : IContentFilePathSplitter
    {
        public IEnumerable<ContentFilePath> SplitContentFilePath(ContentFilePath path)
        {
            var pathSections = ((string)(path ?? "")).Split('/');

            for (int i = 0; i < pathSections.Length; i++)
            {
                var sectionsForThisPath = pathSections.Where((x, index) => index <= i);
                var sectionPath = string.Join("/", sectionsForThisPath);
                yield return sectionPath;
            }
        }
    }
}
