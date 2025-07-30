using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Charipay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=NISHATS-IDEAPAD; Database=Charipay-DB;User Id=nazmul.hyder;password=nazmul496; MultipleActiveResultSets=True; TrustServerCertificate=True");
            });
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
