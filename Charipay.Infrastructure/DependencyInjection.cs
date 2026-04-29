using Charipay.Application.Interfaces.QueryService;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Application.Interfaces.Storage;
using Charipay.Infrastructure.Data;
using Charipay.Infrastructure.Persistence;
using Charipay.Infrastructure.QueryService;
using Charipay.Infrastructure.Repositories;
using Charipay.Infrastructure.Security;
using Charipay.Infrastructure.Storage;
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
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("DefaultConnection");

            // DB Connection string
            services.AddDbContext<AppDbContext>(option => { 
             option.UseSqlServer(conn);
            });

            //Blob (read from configuration)
            services.AddScoped<IFileStorageService>(sp =>
            {
                var blobConn = configuration["AzureBlob:ConnectionString"]
                ?? throw new Exception("AzureBlob:ConnectionString is missing");
                
                var container = configuration["AzureBlob:ContainerName"]
                ?? throw new Exception("AzureBlob:ContainerName is missing");

                return new AzureBlobStorageService(blobConn!, container!);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICharityRepository, CharityRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IVolunteerTaskRepository, VolunteerTaskRepository>();
            services.AddScoped<IVolunteerUserRepository, VolunteerUserRepository>();
            services.AddScoped<IVolunteerApplicationHistoryRepository, VolunteerApplicationHistoryRepository>();
            services.AddScoped<IAdminVolunteerRequestQueryService, AdminVolunteerRequestQueryService>();
            services.AddScoped<IPublicRepository, PublicRepository>();
            services.AddScoped<IUserFeedbackRepository, UserFeedbackRepository>();

            //services.AddScoped<IFileStorageService, AzureBlobStorageService>();


            return services;
        }
    }
}
