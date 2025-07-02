using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TCMA.DAL
{
    public static class DALModule
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
