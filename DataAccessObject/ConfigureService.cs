using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using DataAccessObject.IBaseDAO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureDataAccessObjectService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Account>();
            services.AddScoped<Staff>();
            services.AddScoped<Manager>();
            services.AddScoped<Consultant>();
            services.AddScoped<Customer>();
            services.AddScoped<Clinic>();
            services.AddScoped<Category>();
            services.AddScoped<Service>();
            services.AddScoped<Blog>();
            services.AddScoped<TreatmentOutcome>();
            services.AddScoped<Slot>();
            services.AddScoped<WorkingHour>();
            services.AddScoped<Rating>();
            services.AddScoped<Transaction>();
            services.AddScoped<MenstrualCycle>();
            services.AddScoped<CyclePrediction>();
            services.AddScoped<ImageBlog>();
            services.AddScoped<AppointmentDetail>();
            services.AddScoped<Appointment>();
            services.AddScoped<LabTest>();
            services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
            return services;
        }
    }
}
