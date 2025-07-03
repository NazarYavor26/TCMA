using Microsoft.AspNetCore.Mvc;

namespace TCMA.API.Infrastructure.Cache
{
    public static class CacheConfig
    {
        public static void ConfigureCacheProfiles(MvcOptions options, IConfiguration configuration)
        {
            var cacheProfiles = configuration.GetSection("CacheProfiles").GetChildren();

            foreach ( var cacheProfile in cacheProfiles )
            {
                options.CacheProfiles.Add(cacheProfile.Key, cacheProfile.Get<CacheProfile>());
            }
        }
    }
}
