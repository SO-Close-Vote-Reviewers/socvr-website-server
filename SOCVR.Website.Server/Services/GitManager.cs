using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GitManager> logger;

        public GitManager(IProcessRunner processRunnerService, IOptions<Configuration> configOptions, ILogger<GitManager> loggerService)
        {
            processRunner = processRunnerService;
            config = configOptions.Value;
            logger = loggerService;
        }

        public void Clone()
        {
            if (!Directory.Exists(config.CloneDir))
            {
                logger.LogDebug("Creating clone dir before cloning");
                Directory.CreateDirectory(config.CloneDir);
            }

            var args = $"clone --branch {config.GitBranch} {config.GitRepositoryUrl} {config.CloneDir}";
            logger.LogDebug($"Cloning, args: {args}");
            processRunner.Run("git", $"clone --branch {config.GitBranch} {config.GitRepositoryUrl} {config.CloneDir}");
        }

        public bool DoesRepositoryExist()
        {
            if (!Directory.Exists(config.CloneDir))
            {
                logger.LogDebug("Repo dir does not exist");
                return false;
            }

            logger.LogDebug($"Checking if dir is git repo at {config.CloneDir}");
            return processRunner.Run("git", $"rev-parse --is-inside-work-tree", config.CloneDir) == 0;
        }

        public void Pull()
        {
            //note, should not need to checkout out a branch. The only reason the repo's branch would be
            //different than the config value is on a dev machine. In production the container will be recreated
            //and the save repo will be thrown away with it.

            logger.LogDebug($"Pulling {config.GitBranch}");
            processRunner.Run("git", $"pull origin {config.GitBranch}", config.CloneDir);
        }
    }
}
