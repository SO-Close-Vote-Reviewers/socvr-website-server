using SOCVR.Website.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockProcessRunner : IProcessRunner
    {
        private int statusCode;

        public string LastRanProgramName { get; private set; }
        public string LastRanProgramArguments { get; private set; }
        public string LastRanProgramWorkingDirectory { get; private set; }

        public MockProcessRunner(int statusCodeToReturn)
        {
            statusCode = statusCodeToReturn;
        }

        public int Run(string program, string arguments)
        {
            return Run(program, arguments, null);
        }

        public int Run(string program, string arguments, string workingDirectory)
        {
            LastRanProgramName = program;
            LastRanProgramArguments = arguments;
            LastRanProgramWorkingDirectory = workingDirectory;
            return statusCode;
        }
    }
}
