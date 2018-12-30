using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server
{
    public class Configuration
    {
        /// <summary>
        /// The name of the file that is the default for the website. I.E. what to
        /// show if your url path is "/".
        /// </summary>
        public string DefaultMarkdownFileName { get; set; }

        /// <summary>
        /// The folder which contains all markdown files.
        /// Relative to the root of the git repo that is cloned.
        /// </summary>
        public string ContentPageFilesFolder { get; set; }

        /// <summary>
        /// The folder which contains all css style files.
        /// Relative to the root of the git repo that is cloned.
        /// </summary>
        public string StyleFilesFolder { get; set; }

        /// <summary>
        /// Name of the file which controls navigation bar entries.
        /// </summary>
        public string NavigationDataFileName { get; set; }

        /// <summary>
        /// Git clone url for the website content. This should be an http(s) link.
        /// </summary>
        public string GitRepositoryUrl { get; set; }

        /// <summary>
        /// The branch to clone.
        /// </summary>
        public string GitBranch { get; set; }

        /// <summary>
        /// Where to clone the git repo to on this computer.
        /// </summary>
        public string CloneDir { get; set; }

        /// <summary>
        /// The application will wait X many seconds before "git pull"-ing again.
        /// This operation is only potentially performed on a page request.
        /// </summary>
        public int GitPullCacheSeconds { get; set; }

        /// <summary>
        /// Value from Google's Webmaster Portal to help verify you own the website.
        /// </summary>
        public string GoogleSiteVerificationValue { get; set; }
    }
}
