using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public interface IProcessRunner
    {
        int Run(string program, string arguments);

        int Run(string program, string arguments, string workingDirectory);
    }
}
