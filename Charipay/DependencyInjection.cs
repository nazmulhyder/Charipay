using Charipay.Application;
using Charipay.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Charipay.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
           
            services.AddApplicationDI().AddInfrastructureDI();
            return services;
        }
    }
}
