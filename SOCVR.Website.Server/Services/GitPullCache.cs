using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    public class GitPullCache : IGitPullCache
    {
        private readonly Configuration config;
        private readonly IMemoryCache cache;

        private const string CacheKey = "ShouldPullFromGitRepo";

        public GitPullCache(IOptions<Configuration> configOptions, IMemoryCache memoryCache)
        {
            config = configOptions.Value;
            cache = memoryCache;
        }

        public bool ShouldPullRepository()
        {
            if (!cache.TryGetValue(CacheKey, out Guid cacheValue))
            {
                //cache value does not exist, set the timer and return true;
                cacheValue = Guid.NewGuid();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(config.GitPullCacheSeconds));

                cache.Set(CacheKey, cacheValue, cacheEntryOptions);

                return true;
            }

            // there was a cache value
            return false;
        }

        public void Invalidate()
        {
            cache.Remove(CacheKey);
        }
    }
}
