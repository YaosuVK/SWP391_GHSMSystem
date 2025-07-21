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
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IDashBoardRepository, DashBoardRepository>();
            services.AddScoped<ITreatmentOutcomeRepository, TreatmentOutcomeRepository>();
            services.AddScoped<ILabTestRepository, LabTestRepository>();
            



            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IConsultantProfileRepository, ConsultantProfileRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageServiceRepository, ImageServiceRepository>();
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();
            services.AddScoped<IImageBlogRepository, ImageBlogRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IMenstrualCycleRepository, MenstrualCycleRepository>();
            services.AddScoped<ICyclePredictionRepository, CyclePredictionRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IQnAMessageRepository, QnAMessageRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

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
            services.AddScoped<TreatmentOutcomeDAO>();
            services.AddScoped<LabTestDAO>();
            services.AddScoped<FeedBackDAO>();
            services.AddScoped<ConsultantSlotDAO>();
            services.AddScoped<QuestionDAO>();
            services.AddScoped<DashBoardDAO>();
            services.AddScoped<QnAMessageDAO>();
            services.AddScoped<ChatDAO>();
            services.AddScoped<MessageDAO>();
            services.AddScoped<MenstrualCycleDAO>();
            services.AddScoped<CyclePredictionDAO>();
            services.AddScoped<NotificationDAO>();
            services.AddScoped<ConsultantProfileDAO>();
            return services;
        }
    }
}
