using AutoMapper;
using BusinessObject.Model;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.TreatmentOutcomes;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Appointments;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Clinic;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.Transactions;
using Service.RequestAndResponse.Response.TreatmentOutcomes;
using Service.RequestAndResponse.Response.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Appointment, GetAllAppointment>().ReverseMap();
            CreateMap<Appointment, GetAppointmentResponse>().ReverseMap();

            CreateMap<Clinic, ClinicResponse>().ReverseMap();
            CreateMap<CreateClinicRequest, Clinic>().ReverseMap();
            CreateMap<UpdateClinicRequest, Clinic>().ReverseMap();

            CreateMap<WorkingHour, WorkingHourResponse>().ReverseMap();
            CreateMap<CreateWorkingHourRequest, WorkingHour>()
                 .ForMember(dest => dest.OpeningTime, opt => opt.Ignore())
                 .ForMember(dest => dest.ClosingTime, opt => opt.Ignore())
                 .ReverseMap();
            CreateMap<UpdateWorkingHourRequest, WorkingHour>()
                 .ForMember(dest => dest.OpeningTime, opt => opt.Ignore())
                 .ForMember(dest => dest.ClosingTime, opt => opt.Ignore())
                 .ReverseMap();

            CreateMap<Slot, SlotForCustomer>().ReverseMap();
            CreateMap<CreateSlotRequest, Slot>().ReverseMap();
            CreateMap<UpdateSlotRequest, Slot>().ReverseMap();

            CreateMap<Category, GetAllCategoryResponse>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();

            CreateMap<Services, ServicesResponse>().ReverseMap();
            CreateMap<CreateServiceRequest, Services>().ReverseMap();
            CreateMap<UpdateServiceRequest, Services>().ReverseMap();


            CreateMap<TransactionResponse, Transaction>().ReverseMap();

            // TreatmentOutcome mappings
            CreateMap<TreatmentOutcome, GetAllTreatmentOutcomeResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.ConsultantName, opt => opt.MapFrom(src => src.Consultant.FullName))
                .ForMember(dest => dest.LabTestCount, opt => opt.MapFrom(src => src.LabTests.Count()));

            CreateMap<TreatmentOutcome, GetTreatmentOutcomeByIdResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                .ForMember(dest => dest.ConsultantName, opt => opt.MapFrom(src => src.Consultant.FullName))
                .ForMember(dest => dest.ConsultantEmail, opt => opt.MapFrom(src => src.Consultant.Email));

            CreateMap<CreateTreatmentOutcomeRequest, TreatmentOutcome>()
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateTreatmentOutcomeRequest, TreatmentOutcome>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // LabTest mappings
            CreateMap<LabTest, GetAllLabTestResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.FullName));

            CreateMap<LabTest, GetLabTestByIdResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.FullName))
                .ForMember(dest => dest.StaffEmail, opt => opt.MapFrom(src => src.Staff.Email))
                .ForMember(dest => dest.TreatmentDiagnosis, opt => opt.MapFrom(src => src.TreatmentOutcome.Diagnosis));

            CreateMap<CreateLabTestRequest, LabTest>();

            CreateMap<UpdateLabTestRequest, LabTest>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
