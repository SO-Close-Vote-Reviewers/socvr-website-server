using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server
{
    public class Configuration
    {
        public string DefaultMarkdownFileName { get; set; }
        public string ContentPageFilesFolder { get; set; }
        public string StyleFilesFolder { get; set; }
        public string NavigationDataFileName { get; set; }
        public string GitRepositoryUrl { get; set; }
        public string GitBranch { get; set; }
        public string CloneDir { get; set; }
        public int GitPullCacheSeconds { get; set; }
    }
}
