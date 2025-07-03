using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace TCMA.API.Infrastructure.RateLimit
{
    public static class RateLimiterConfig
    {
        public static void AddConfiguredRateLimiter(this IServiceCollection services, IConfiguration configuration)
        {
            var rateLimitingOptions = configuration.GetSection("RateLimiting").Get<RateLimiterOptions>();

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddConcurrencyLimiter("writeOperations", limiteroptions =>
                {
                    limiteroptions.PermitLimit = rateLimitingOptions.WriteOperations.PermitLimit;
                    limiteroptions.QueueLimit = rateLimitingOptions.WriteOperations.QueueLimit;
                    limiteroptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddSlidingWindowLimiter("readOperations", limiteroptions =>
                {
                    limiteroptions.PermitLimit = rateLimitingOptions.ReadOperations.PermitLimit;
                    limiteroptions.Window = TimeSpan.FromSeconds(rateLimitingOptions.ReadOperations.WindowSeconds);
                    limiteroptions.SegmentsPerWindow = rateLimitingOptions.ReadOperations.SegmentsPerWindow;
                    limiteroptions.QueueLimit = rateLimitingOptions.ReadOperations.QueueLimit;
                    limiteroptions.PermitLimit = rateLimitingOptions.ReadOperations.PermitLimit;
                    limiteroptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
        }

        public static IApplicationBuilder UseConfiguredRateLimiter(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }
    }
}
