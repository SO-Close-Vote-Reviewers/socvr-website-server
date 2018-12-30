using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server
{
    /// <summary>
    /// Helper class to let Application Insights know that a piece of logic should be treated as a dependency.
    /// </summary>
    public static class Dependency
    {
        /// <summary>
        /// Let Application Insights know that a piece of logic should be treated as a dependency.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the <paramref name="dependencyCall"/> function.</typeparam>
        /// <param name="telemetryClient"></param>
        /// <param name="dependencyTypeName">External dependency type. Very low cardinality value for logical grouping 
        /// and interpretation of fields. Examples are SQL, Azure table, and HTTP.</param>
        /// <param name="dependencyName">Name of the command initiated with this dependency call. Low cardinality value. 
        /// Examples are stored procedure name and URL path template.</param>
        /// <param name="data">Command initiated by this dependency call. Examples are SQL statement and HTTP URL's with all query parameters.</param>
        /// <param name="dependencyCall"></param>
        /// <returns></returns>
        public static TResult RunDependency<TResult>(TelemetryClient telemetryClient, string dependencyTypeName, string dependencyName,
            string data, Func<TResult> dependencyCall)
        {
            var dependencyWasSuccessful = false;
            var start = DateTimeOffset.UtcNow;
            TResult results;
            try
            {
                results = dependencyCall();
                dependencyWasSuccessful = true;
                return results;
            }
            finally
            {
                var end = DateTimeOffset.UtcNow;
                telemetryClient?.TrackDependency(dependencyTypeName, dependencyName, data, start, end - start, dependencyWasSuccessful);
            }
        }

        /// <summary>
        /// Let Application Insights know that a piece of logic should be treated as a dependency.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the <paramref name="dependencyCall"/> function.</typeparam>
        /// <param name="telemetryClient"></param>
        /// <param name="dependencyTypeName">External dependency type. Very low cardinality value for logical grouping 
        /// and interpretation of fields. Examples are SQL, Azure table, and HTTP.</param>
        /// <param name="dependencyName">Name of the command initiated with this dependency call. Low cardinality value. 
        /// Examples are stored procedure name and URL path template.</param>
        /// <param name="data">Command initiated by this dependency call. Examples are SQL statement and HTTP URL's with all query parameters.</param>
        /// <param name="dependencyCall"></param>
        /// <returns></returns>
        public static async Task<TResult> RunDependencyAsync<TResult>(TelemetryClient telemetryClient, string dependencyTypeName, string dependencyName,
            string data, Func<Task<TResult>> dependencyCall)
        {
            var dependencyWasSuccessful = false;
            var start = DateTimeOffset.UtcNow;
            TResult results;
            try
            {
                results = await dependencyCall();
                dependencyWasSuccessful = true;
                return results;
            }
            finally
            {
                var end = DateTimeOffset.UtcNow;
                telemetryClient?.TrackDependency(dependencyTypeName, dependencyName, data, start, end - start, dependencyWasSuccessful);
            }
        }

        /// <summary>
        /// Let Application Insights know that a piece of logic should be treated as a dependency.
        /// </summary>
        /// <param name="telemetryClient"></param>
        /// <param name="dependencyTypeName">External dependency type. Very low cardinality value for logical grouping 
        /// and interpretation of fields. Examples are SQL, Azure table, and HTTP.</param>
        /// <param name="dependencyName">Name of the command initiated with this dependency call. Low cardinality value. 
        /// Examples are stored procedure name and URL path template.</param>
        /// <param name="data">Command initiated by this dependency call. Examples are SQL statement and HTTP URL's with all query parameters.</param>
        /// <param name="dependencyCall"></param>
        /// <returns></returns>
        public static async Task RunDependencyAsync(TelemetryClient telemetryClient, string dependencyTypeName, string dependencyName, string data,
            Func<Task> dependencyCall)
        {
            bool dependencyWasSuccessful = false;
            var start = DateTimeOffset.UtcNow;
            try
            {
                await dependencyCall();
                dependencyWasSuccessful = true;
            }
            finally
            {
                var end = DateTimeOffset.UtcNow;
                telemetryClient?.TrackDependency(dependencyTypeName, dependencyName, data, start, end - start, dependencyWasSuccessful);
            }
        }
    }
}
