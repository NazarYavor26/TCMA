using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace TCMA.API.Infrastructure.Extensions
{
    public static class ResponseCompressionExtension
    {
        public static IServiceCollection AddGzipResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            return services;
        }
    }
}
