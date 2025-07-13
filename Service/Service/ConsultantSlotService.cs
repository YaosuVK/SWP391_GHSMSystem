using AutoMapper;
using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Response.ConsultantSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ConsultantSlotService : IConsultantSlotService
    {
        private readonly IConsultantSlotRepository _repo;
        private readonly ISlotRepository _slotRepo;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;

        public ConsultantSlotService(
            IConsultantSlotRepository repo,
            ISlotRepository slotRepo,
            UserManager<Account> userManager, IMapper mapper)
        {
            _repo = repo;
            _slotRepo = slotRepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ConsultantSlot>> RegisterAsync(
            string consultantId,
            int slotId,
            int maxAppointment)
        {
            // --- 1. Xác thực consultant ---
            var consultant = await _userManager.FindByIdAsync(consultantId);
            if (consultant == null)
                return new BaseResponse<ConsultantSlot>(
                    "Consultant not found",
                    StatusCodeEnum.NotFound_404,
                    null);

            // --- 2. Xác thực slot tồn tại ---
            var slot = await _slotRepo.GetByIdAsync(slotId);
            if (slot == null)
                return new BaseResponse<ConsultantSlot>(
                    "Slot not found",
                    StatusCodeEnum.NotFound_404,
                    null);

            // --- 3. Kiểm tra đã đăng ký chưa ---
            var exist = await _repo.GetByConsultantAndSlotAsync(consultantId, slotId);
            if (exist != null)
                return new BaseResponse<ConsultantSlot>(
                    "Already registered for this slot",
                    StatusCodeEnum.Conflict_409,
                    null);

            // --- 4. Kiểm tra slot còn sức chứa ---
            int currentCount = slot.ConsultantSlots?.Count() ?? 0;
            if (currentCount >= slot.MaxConsultant)
                return new BaseResponse<ConsultantSlot>(
                    "Slot is full",
                    StatusCodeEnum.Conflict_409,
                    null);

            // --- 5. Tạo ConsultantSlot ---
            var cs = new ConsultantSlot
            {
                ConsultantID = consultantId,
                SlotID = slotId,
                AssignedDate = DateTime.UtcNow,
                MaxAppointment = maxAppointment
            };
            var added = await _repo.AddSlotAsync(cs);

            return new BaseResponse<ConsultantSlot>(
                "Registered successfully",
                StatusCodeEnum.Created_201,
                added);
        }

        // 2) Trao đổi ca
        public async Task<BaseResponse<bool>> SwapAsync(
    string consultantIdA, int slotIdA,
    string consultantIdB, int slotIdB)
        {
            // 1) Kiểm tra input
            if (consultantIdA == consultantIdB)
                return new BaseResponse<bool>("Cannot swap the same consultant", StatusCodeEnum.BadRequest_400, false);
            if (slotIdA == slotIdB)
                return new BaseResponse<bool>("Cannot swap the same slot", StatusCodeEnum.BadRequest_400, false);

            // 2) Lấy hai bản ghi cũ
            var csA = await _repo.GetByConsultantAndSlotAsync(consultantIdA, slotIdA);
            var csB = await _repo.GetByConsultantAndSlotAsync(consultantIdB, slotIdB);
            if (csA == null || csB == null)
                return new BaseResponse<bool>("One or both registrations not found", StatusCodeEnum.NotFound_404, false);

            // 3) Xóa hai bản ghi cũ
            await _repo.DeleteAsync(csA);
            await _repo.DeleteAsync(csB);

            // 4) Tạo hai bản ghi mới (hoán đổi consultant)
            var swappedA = new ConsultantSlot
            {
                ConsultantID = consultantIdB,
                SlotID = slotIdA,
                AssignedDate = csA.AssignedDate,
                MaxAppointment = csA.MaxAppointment
            };
            var swappedB = new ConsultantSlot
            {
                ConsultantID = consultantIdA,
                SlotID = slotIdB,
                AssignedDate = csB.AssignedDate,
                MaxAppointment = csB.MaxAppointment
            };

            // 5) Lưu hai bản ghi mới
            await _repo.AddAsync(swappedA);
            await _repo.AddAsync(swappedB);

            return new BaseResponse<bool>("Swap successful", StatusCodeEnum.OK_200, true);
        }



        public async Task<BaseResponse<IEnumerable<ConsultantSlotResponse>>> GetRegisteredSlotsAsync(string consultantId)
        {
            // 1. Lấy danh sách booking của consultant
            var list = await _repo.GetByConsultantAsync(consultantId);

            // 2. Nếu không tìm thấy, trả về 404
            if (list == null || !list.Any())
                return new BaseResponse<IEnumerable<ConsultantSlotResponse>>(
                    "No registrations found",
                    StatusCodeEnum.NotFound_404,
                    null);

            var mappedList = _mapper.Map<IEnumerable<ConsultantSlotResponse>>(list);

            // 3. Trả về danh sách
            return new BaseResponse<IEnumerable<ConsultantSlotResponse>>(
                "Registered slots fetched successfully",
                StatusCodeEnum.OK_200,
                mappedList);
        }

        public async Task<BaseResponse<IEnumerable<ConsultantSlotResponse>>> GetAllAsync()
        {
            var list = await _repo.GetAllConsultantSlot();
            if (list == null || !list.Any())
                return new BaseResponse<IEnumerable<ConsultantSlotResponse>>("No consultant slots found", StatusCodeEnum.NotFound_404, null);

            var mappedList = _mapper.Map<IEnumerable<ConsultantSlotResponse>>(list);

            return new BaseResponse<IEnumerable<ConsultantSlotResponse>>("Get all slot success", StatusCodeEnum.OK_200, mappedList);
        }

        public async Task<BaseResponse<IEnumerable<ConsultantSlotResponse>>> SearchAsync(string consultantKeyword, DateTime? date)
        {
            // Gọi repo để tìm theo keyword và/hoặc ngày
            var list = await _repo.SearchAsync(consultantKeyword, date);

            if (list == null || !list.Any())
            {
                return new BaseResponse<IEnumerable<ConsultantSlotResponse>>(
                    "No consultant slots found matching your search criteria!",
                    StatusCodeEnum.NotFound_404,
                    null
                );
            }

            var mappedList = _mapper.Map<IEnumerable<ConsultantSlotResponse>>(list);

            if (mappedList == null || !mappedList.Any())
            {
                return new BaseResponse<IEnumerable<ConsultantSlotResponse>>(
                    "Something went wrong during mapping!",
                    StatusCodeEnum.BadGateway_502,
                    null
                );
            }

            return new BaseResponse<IEnumerable<ConsultantSlotResponse>>(
                "Get consultant slots by search criteria success",
                StatusCodeEnum.OK_200,
                mappedList
            );
        }
    }
}
