using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCMA.BLL.Services;

namespace TCMA.BLL
{
    public static class BLLModule
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IComponentService, ComponentService>();
            return services;
        }
    }
}
