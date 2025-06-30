using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.Mapping;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureServiceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<IWorkingHourService, WorkingHourService>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IServiceService, ServicesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IConsultantSlotService, ConsultantSlotService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IConsultantProfileServive, ConsultantProfileService>();


            return services;
        }
    }
}
