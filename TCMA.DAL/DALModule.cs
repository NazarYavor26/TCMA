using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCMA.DAL.DbContexts;
using TCMA.DAL.Repositories;

namespace TCMA.DAL
{
    public static class DALModule
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DBConnection")), ServiceLifetime.Singleton);

            services.AddTransient<IComponentRepository, ComponentRepository>();

            return services;
        }
    }
}
