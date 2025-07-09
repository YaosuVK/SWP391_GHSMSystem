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
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IWorkingHourRepository, WorkingHourRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IConsultantSlotRepository, ConsultantSlotRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IDashBoardRepository, DashBoardRepository>();

            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IConsultantProfileRepository, ConsultantProfileRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageServiceRepository, ImageServiceRepository>();
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();
            services.AddScoped<IImageBlogRepository, ImageBlogRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<AccountDAO>();
            services.AddScoped<BlogDAO>();
            services.AddScoped<AppointmentDAO>();
            services.AddScoped<AppointmentDetailDAO>();
            services.AddScoped<ClinicDAO>();
            services.AddScoped<CategoryDAO>();
            services.AddScoped<ServiceDAO>();
            services.AddScoped<SlotDAO>();
            services.AddScoped<WorkingHourDAO>();
            services.AddScoped<ImageServicesDAO>();
            services.AddScoped<ImageBlogDAO>();
            services.AddScoped<TransactionDAO>();
            services.AddScoped<FeedBackDAO>();
            services.AddScoped<ConsultantSlotDAO>();
            services.AddScoped<MessageDAO>();
            services.AddScoped<QuestionDAO>();
            services.AddScoped<DashBoardDAO>();

            services.AddScoped<ConsultantProfileDAO>();
            return services;
        }
    }
}
