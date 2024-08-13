



using Business.Implement;
using Business.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Prepare
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<MrcContext>>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<MrcContext>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
         

            return services;
        }


        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefautDB"];

            return strConn;
        }
    }
}
