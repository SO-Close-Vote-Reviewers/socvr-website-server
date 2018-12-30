using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly TelemetryClient telemetryClient;

        public ProcessRunner(TelemetryClient telemetryClient = null)
        {
            this.telemetryClient = telemetryClient;
        }

        public int Run(string program, string arguments)
        {
            return Run(program, arguments, null);
        }

        public int Run(string program, string arguments, string workingDirectory)
        {
            var info = new ProcessStartInfo(program)
            {
                Arguments = arguments
            };

            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                info.WorkingDirectory = workingDirectory;
            }

            var processExitCode = Dependency.RunDependency(
                telemetryClient,
                "System Process",
                program,
                arguments,
                () =>
                {
                    var process = Process.Start(info);
                    process.WaitForExit();
                    return process.ExitCode;
                });

            return processExitCode;
        }
    }
}
