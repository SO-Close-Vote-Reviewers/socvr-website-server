using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOCVR.Website.Server.Tests.Mock
{
    class MockLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //don't care. do nothing.
        }
    }
}
