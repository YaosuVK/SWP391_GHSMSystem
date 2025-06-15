using AutoMapper;
using BusinessObject.Model;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Clinic;
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
            CreateMap<Clinic, ClinicResponse>().ReverseMap();
            CreateMap<CreateClinicRequest, Clinic>().ReverseMap();
            CreateMap<UpdateClinicRequest, Clinic>().ReverseMap();

            CreateMap<WorkingHour, WorkingHourResponse>().ReverseMap();
            CreateMap<CreateWorkingHourRequest, WorkingHour>().ReverseMap();
            CreateMap<UpdateWorkingHourRequest, WorkingHour>().ReverseMap();

            CreateMap<Slot, SlotForCustomer>().ReverseMap();
            CreateMap<CreateSlotRequest, Slot>().ReverseMap();
            CreateMap<UpdateSlotRequest, Slot>().ReverseMap();

            CreateMap<TransactionResponse, Transaction>().ReverseMap();

        }
    }
}
