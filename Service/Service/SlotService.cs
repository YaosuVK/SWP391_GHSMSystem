using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SlotService :  ISlotService
    {
        private readonly IMapper _mapper;
        private readonly ISlotRepository _slotRepository;
        private readonly IWorkingHourRepository _workingHourRepository;
        private readonly IClinicRepository _clinicalRepository;

        public SlotService(IMapper mapper, ISlotRepository slotRepository, IWorkingHourRepository workingHourRepository, IClinicRepository clinicalRepository)
        {
            _mapper = mapper;
            _slotRepository = slotRepository;
            _workingHourRepository = workingHourRepository;
            _clinicalRepository = clinicalRepository;
        }

        public async Task<BaseResponse<List<Slot>>> AddAsync(CreateSlotRequest entity)
        {
            var workingHour = await _workingHourRepository.GetWorkingHourById(entity.WorkingHourID);
            if(workingHour == null)
            {
                return new BaseResponse<List<Slot>>("Cannot find the working hour", StatusCodeEnum.NotFound_404, null);
            }

            var clinic = await _clinicalRepository.GetClinicById(entity.ClinicID);
            if (clinic == null)
            {
                return new BaseResponse<List<Slot>>("Cannot find the clinic", StatusCodeEnum.NotFound_404, null);
            }

            if (entity.MaxConsultant <= 0)
            {
                return new BaseResponse<List<Slot>>("MaxConsultant must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.MaxTestAppointment <= 0)
            {
                return new BaseResponse<List<Slot>>("MaxTestAppointment must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.StartTime >= entity.EndTime)
            {
                return new BaseResponse<List<Slot>>("Start time must < End Time", StatusCodeEnum.BadRequest_400, null);
            }

            TimeOnly startTime = TimeOnly.FromDateTime(entity.StartTime);
            TimeOnly endTime = TimeOnly.FromDateTime(entity.EndTime);

            if (startTime < workingHour.OpeningTime || endTime > workingHour.ClosingTime)
            {
                return new BaseResponse<List<Slot>>("Slot time must be within working hours", StatusCodeEnum.NotAcceptable_406, null);
            }

             var existingSlots = await _slotRepository.GetSlotsByWorkingHourId(entity.WorkingHourID);
                foreach (var existing in existingSlots)
                {
                    if (entity.StartTime < existing.EndTime && entity.EndTime > existing.StartTime)
                    {
                        return new BaseResponse<List<Slot>>("Slot time overlaps with an existing slot", StatusCodeEnum.Conflict_409, null);
                    }
                }

            var slotList = new List<Slot>()
            {
                new Slot
                    {
                        ClinicID = entity.ClinicID,
                        WorkingHourID = entity.WorkingHourID,
                        MaxConsultant = entity.MaxConsultant,
                        MaxTestAppointment = entity.MaxTestAppointment,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    }
            };
                await _slotRepository.AddRange(slotList);
            

            return new BaseResponse<List<Slot>>("Create slot success", StatusCodeEnum.Created_201, slotList);
        }

        public async Task<BaseResponse<IEnumerable<SlotForCustomer>>> GetAllAsync()
        {
            IEnumerable<Slot> slot = await _slotRepository.GetAllAsync();
            if (slot == null || !slot.Any())
            {
                return new BaseResponse<IEnumerable<SlotForCustomer>>("No slot found!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var slots = _mapper.Map<IEnumerable<SlotForCustomer>>(slot);
            if (slots == null || !slots.Any())
            {
                return new BaseResponse<IEnumerable<SlotForCustomer>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<SlotForCustomer>>("Get all slot success",
                StatusCodeEnum.OK_200, slots);
        }

        public async Task<BaseResponse<IEnumerable<SlotForCustomer>>> SearchSlotsAsync(string keyword)
        {
            IEnumerable<Slot> slot = await _slotRepository.SearchSlotsAsync(keyword);
            if (slot == null || !slot.Any())
            {
                return new BaseResponse<IEnumerable<SlotForCustomer>>("No slot found matching your search!",
                StatusCodeEnum.NotFound_404, null);
            }
            var slots = _mapper.Map<IEnumerable<SlotForCustomer>>(slot);
            if (slots == null || !slots.Any())
            {
                return new BaseResponse<IEnumerable<SlotForCustomer>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<SlotForCustomer>>("Get slots by keyword success",
                StatusCodeEnum.OK_200, slots);
        }

        public async Task<BaseResponse<Slot>> UpdateAsync(int slotID, UpdateSlotRequest entity)
        {
            var existSlot = await _slotRepository.GetByIdAsync(slotID);
            if (existSlot == null)
            {
                return new BaseResponse<Slot>("Cannot find the slot", StatusCodeEnum.NotFound_404, null);
            }

            var workingHour = await _workingHourRepository.GetWorkingHourById(entity.WorkingHourID);
            if (workingHour == null)
            {
                return new BaseResponse<Slot>("Cannot find the working hour", StatusCodeEnum.NotFound_404, null);
            }

            if (entity.MaxConsultant <= 0)
            {
                return new BaseResponse<Slot>("MaxConsultant must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.MaxTestAppointment <= 0)
            {
                return new BaseResponse<Slot>("MaxTestAppointment must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.StartTime >= entity.EndTime)
            {
                return new BaseResponse<Slot>("Start time must < End Time", StatusCodeEnum.BadRequest_400, null);
            }

            TimeOnly startTime = TimeOnly.FromDateTime(entity.StartTime);
            TimeOnly endTime = TimeOnly.FromDateTime(entity.EndTime);
            if (startTime < workingHour.OpeningTime || endTime > workingHour.ClosingTime)
            {
                return new BaseResponse<Slot>("Slot time must be within working hours", StatusCodeEnum.NotAcceptable_406, null);
            }
            _mapper.Map(entity, existSlot);
            existSlot.UpdateAt = DateTime.UtcNow;

            // 3. Cập nhật
            await _slotRepository.UpdateAsync(existSlot);
            return new BaseResponse<Slot>("Update slot success.", StatusCodeEnum.OK_200, existSlot);
        }

        public async Task<BaseResponse<Slot>> DeleteAsync(int slotId)
        {
            var slot = await _slotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return new BaseResponse<Slot>("Slot not found", StatusCodeEnum.NotFound_404, null);

            await _slotRepository.DeleteAsync(slot);
            return new BaseResponse<Slot>("Slot deleted", StatusCodeEnum.OK_200, null);
        }
    }
}
