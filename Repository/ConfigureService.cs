using DataAccessObject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.BaseRepository;
using Repository.IBaseRepository;
using Repository.IRepositories;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureRepositoryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            
            services.AddScoped<IConsultantRepository, ConsultantRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<AccountDAO>();
            services.AddScoped<ConsultantProfileDAO>();
            return services;
        }
    }
}
