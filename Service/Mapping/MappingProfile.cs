using AutoMapper;
using BusinessObject.Model;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Appointments;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Clinic;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.Transactions;
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

        }
    }
}
