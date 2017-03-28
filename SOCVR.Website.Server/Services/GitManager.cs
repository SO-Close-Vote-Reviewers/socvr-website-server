using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class GitManager : IGitManager
    {
        private readonly IProcessRunner processRunner;
        private readonly Configuration config;

        public GitManager(IProcessRunner processRunnerService, IOptions<Configuration> configOptions)
        {
            processRunner = processRunnerService;
            config = configOptions.Value;
        }

        public void Clone()
        {
            if (!Directory.Exists(config.CloneDir))
                Directory.CreateDirectory(config.CloneDir);

            processRunner.Run("git", $"clone --branch {config.GitBranch} {config.GitRepositoryUrl} {config.CloneDir}");
        }

        public bool DoesRepositoryExist()
        {
            if (!Directory.Exists(config.CloneDir))
                return false;

            return processRunner.Run("git", $"rev-parse --is-inside-work-tree", config.CloneDir) == 0;
        }

        public void Pull()
        {
            processRunner.Run("git", $"checkout {config.GitBranch}");
            processRunner.Run("git", $"pull origin {config.GitBranch}", config.CloneDir);
        }
    }
}
