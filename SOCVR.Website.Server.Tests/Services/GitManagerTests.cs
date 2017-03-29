using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SOCVR.Website.Server.Tests.Services
{
    public class GitManagerTests
    {
        [Fact]
        public void Clone_ThrowsOnBadExit()
        {
            var runner = new MockProcessRunner(20);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(true);
            var gitManager = new GitManager(runner, config, logger, dir);

            Assert.Throws<InvalidOperationException>(() => gitManager.Clone());
        }

        [Fact]
        public void Clone_CorrectProcessParams()
        {
            var runner = new MockProcessRunner(0);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(true);
            var gitManager = new GitManager(runner, config, logger, dir);

            gitManager.Clone();

            var expectedProgramName = "git";
            var expectedProgramArgs = $"clone --branch {config.Value.GitBranch} {config.Value.GitRepositoryUrl} {config.Value.CloneDir}";

            Assert.Equal(expectedProgramName, runner.LastRanProgramName);
            Assert.Equal(expectedProgramArgs, runner.LastRanProgramArguments);
            Assert.Null(dir.LastCreatedDirectoryPath);
        }

        [Fact]
        public void Clone_CloneDirDoesNotExist()
        {
            var runner = new MockProcessRunner(0);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(false);
            var gitManager = new GitManager(runner, config, logger, dir);

            gitManager.Clone();

            var expectedProgramName = "git";
            var expectedProgramArgs = $"clone --branch {config.Value.GitBranch} {config.Value.GitRepositoryUrl} {config.Value.CloneDir}";

            Assert.Equal(expectedProgramName, runner.LastRanProgramName);
            Assert.Equal(expectedProgramArgs, runner.LastRanProgramArguments);
            Assert.Equal(config.Value.CloneDir, dir.LastCreatedDirectoryPath);
        }

        [Fact]
        public void DoesRepositoryExist_CorrectProcessParams()
        {
            var runner = new MockProcessRunner(0);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(true);
            var gitManager = new GitManager(runner, config, logger, dir);

            var actualExists = gitManager.DoesRepositoryExist();
            var expectedExists = true;

            Assert.Equal(expectedExists, actualExists);

            var expectedProgramName = "git";
            var expectedProgramArgs = $"rev-parse --is-inside-work-tree";

            Assert.Equal(expectedProgramName, runner.LastRanProgramName);
            Assert.Equal(expectedProgramArgs, runner.LastRanProgramArguments);
            Assert.Equal(config.Value.CloneDir, runner.LastRanProgramWorkingDirectory);
        }

        [Fact]
        public void DoesRepositoryExist_CloneDirDoesNotExist()
        {
            var runner = new MockProcessRunner(0);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(false);
            var gitManager = new GitManager(runner, config, logger, dir);

            var actualExists = gitManager.DoesRepositoryExist();
            var expectedExists = false;

            Assert.Equal(expectedExists, actualExists);

            Assert.Null(runner.LastRanProgramName);
            Assert.Null(runner.LastRanProgramArguments);
            Assert.Null(runner.LastRanProgramWorkingDirectory);
        }

        [Fact]
        public void Pull_CorrectProcessParams()
        {
            var runner = new MockProcessRunner(0);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(true);
            var gitManager = new GitManager(runner, config, logger, dir);

            gitManager.Pull();

            var expectedProgramName = "git";
            var expectedProgramArgs = $"pull origin {config.Value.GitBranch}";

            Assert.Equal(expectedProgramName, runner.LastRanProgramName);
            Assert.Equal(expectedProgramArgs, runner.LastRanProgramArguments);
            Assert.Equal(config.Value.CloneDir, runner.LastRanProgramWorkingDirectory);
        }

        [Fact]
        public void Pull_ThrowOnBadExit()
        {
            var runner = new MockProcessRunner(3);
            var config = new MockConfigurationOptions();
            var logger = new MockLogger<GitManager>();
            var dir = new MockDirectoryProvider(true);
            var gitManager = new GitManager(runner, config, logger, dir);

            Assert.Throws<InvalidOperationException>(() => gitManager.Pull());
        }
    }
}
