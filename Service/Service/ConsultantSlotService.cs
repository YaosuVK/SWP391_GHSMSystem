using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
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

        public ConsultantSlotService(
            IConsultantSlotRepository repo,
            ISlotRepository slotRepo,
            UserManager<Account> userManager)
        {
            _repo = repo;
            _slotRepo = slotRepo;
            _userManager = userManager;
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
                string consA, int slotA,
                string consB, int slotB)
        {
            // a. Lấy hai booking
            var a = await _repo.GetByConsultantAndSlotAsync(consA, slotA);
            var b = await _repo.GetByConsultantAndSlotAsync(consB, slotB);
            if (a == null || b == null)
                return new BaseResponse<bool>("One or both registrations not found", StatusCodeEnum.NotFound_404, false);

            // b. Hoán đổi ConsultantID
            var temp = a.ConsultantID;
            a.ConsultantID = b.ConsultantID;
            b.ConsultantID = temp;

            // c. Lưu thay đổi
            await _repo.DeleteAsync(a);
            await _repo.DeleteAsync(b);
            await _repo.AddAsync(a);
            await _repo.AddAsync(b);
            return new BaseResponse<bool>("Swap successful", StatusCodeEnum.OK_200, true);
        }

        public async Task<BaseResponse<IEnumerable<ConsultantSlot>>> GetRegisteredSlotsAsync(string consultantId)
        {
            // 1. Lấy danh sách booking của consultant
            var list = await _repo.GetByConsultantAsync(consultantId);

            // 2. Nếu không tìm thấy, trả về 404
            if (list == null || !list.Any())
                return new BaseResponse<IEnumerable<ConsultantSlot>>(
                    "No registrations found",
                    StatusCodeEnum.NotFound_404,
                    null);

            // 3. Trả về danh sách
            return new BaseResponse<IEnumerable<ConsultantSlot>>(
                "Registered slots fetched successfully",
                StatusCodeEnum.OK_200,
                list);
        }

        public async Task<BaseResponse<IEnumerable<ConsultantSlot>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            if (list == null || !list.Any())
                return new BaseResponse<IEnumerable<ConsultantSlot>>("No consultant slots found", StatusCodeEnum.NotFound_404, null);
            return new BaseResponse<IEnumerable<ConsultantSlot>>("All consultant slots fetched", StatusCodeEnum.OK_200, list);
        }
    }
}
