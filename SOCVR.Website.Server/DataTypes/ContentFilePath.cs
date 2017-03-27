using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.DataTypes
{
    /// <summary>
    /// A relative path to a file within the Content repository. This value originates from the browser URL.
    /// It must be translated into a physical path to be looked up.
    /// A null value mean the "root" path.
    /// </summary>
    public class ContentFilePath
    {
        public string Value { get; private set; }

        public ContentFilePath(string path)
        {
            Value = path;
        }

        public static implicit operator string(ContentFilePath path) => path?.Value;
        public static implicit operator ContentFilePath(string path) => new ContentFilePath(path);
    }
}
