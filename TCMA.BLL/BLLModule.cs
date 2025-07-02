using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TCMA.BLL
{
    public static class BLLModule
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
