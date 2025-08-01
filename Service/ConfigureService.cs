﻿using Microsoft.Extensions.Configuration;
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
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<IWorkingHourService, WorkingHourService>();
            services.AddScoped<IAutoCheckoutAppointmentService, AutoCheckoutAppointmentService>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IServiceService, ServicesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITreatmentOutcomeService, TreatmentOutcomeService>();
            services.AddScoped<ILabTestService, LabTestService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<IConsultantSlotService, ConsultantSlotService>();
            services.AddScoped<IConsultantProfileServive, ConsultantProfileService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMenstrualCycleService, MenstrualCycleService>();
            services.AddScoped<ICyclePredictionService, CyclePredictionService>();
            services.AddScoped<IQnAMessageService, QnAMessageService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IQuestionService, QuestionService>();
            return services;
        }
    }
}
