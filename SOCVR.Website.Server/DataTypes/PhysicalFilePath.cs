using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.DataTypes
{
    /// <summary>
    /// Represents a path of a file on the file system. This path will be used to read the file.
    /// </summary>
    public class PhysicalFilePath
    {
        private string value;

        public PhysicalFilePath(string path)
        {
            value = path;
        }

        public static implicit operator string(PhysicalFilePath path) => path.value;
        public static implicit operator PhysicalFilePath(string path) => new PhysicalFilePath(path);
    }
}
