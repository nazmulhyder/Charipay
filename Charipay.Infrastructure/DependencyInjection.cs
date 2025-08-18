using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Charipay.Infrastructure.Persistence;
using Charipay.Infrastructure.Repositories;
using Charipay.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                //local
                //options.UseSqlServer("Server=NISHATS-IDEAPAD; Database=Charipay-DB;User Id=nazmul.hyder;password=nazmul496; MultipleActiveResultSets=True; TrustServerCertificate=True");
                //azure sql
                 options.UseSqlServer("Server=tcp:charipay-dbdbserver.database.windows.net,1433;Initial Catalog=Charipay-DB;Persist Security Info=False;User ID=nazmul.hyder;Password=@Nazder496;");

            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
          

            return services;
        }
    }
}
