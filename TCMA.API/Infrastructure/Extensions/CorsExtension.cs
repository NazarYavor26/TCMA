namespace TCMA.API.Infrastructure.Extensions
{
    public static class CorsExtension
    {
        private const string TCMA_POLICY = "TCMAPolicy";

        public static string PolicyName => TCMA_POLICY;

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(TCMA_POLICY, policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Token-Expired");
                });
            });

            return services;
        }
    }
}
