using BusinessObject.Model;
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

        public ConsultantSlotService(
             IConsultantSlotRepository repo,
             ISlotRepository slotRepo)
        {
            _repo = repo;
            _slotRepo = slotRepo;
        }

        // 1) Đăng ký slot
        public async Task<BaseResponse<ConsultantSlot>> RegisterAsync(string consultantId, int slotId)
        {
            // a. Kiểm tra slot tồn tại
            var slot = await _slotRepo.GetByIdAsync(slotId);
            if (slot == null)
                return new BaseResponse<ConsultantSlot>("Slot not found", StatusCodeEnum.NotFound_404, null);

            // b. Kiểm tra consultant chưa đăng ký
            var exist = await _repo.GetByConsultantAsync(consultantId);
            if (exist != null)
                return new BaseResponse<ConsultantSlot>("Already registered", StatusCodeEnum.Conflict_409, null);

            // c. Kiểm tra số lượng consultant đã đăng ký < max
            int count = slot.ConsultantSlots?.Count() ?? 0;
            if (count >= slot.MaxConsultant)
                return new BaseResponse<ConsultantSlot>("Slot is full", StatusCodeEnum.Conflict_409, null);

            // d. Tạo booking
            var cs = new ConsultantSlot
            {
                ConsultantID = consultantId,
                SlotID = slotId,
                AssignedDate = DateTime.UtcNow,
                MaxAppointment = slot.MaxConsultant
            };
            var added = await _repo.AddAsync(cs);
            return new BaseResponse<ConsultantSlot>("Registered successfully", StatusCodeEnum.Created_201, added);
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
            await _repo.DeleteAsync(a);  // hoặc _repo.UpdateAsync nếu có
            await _repo.DeleteAsync(b);
            await _repo.AddAsync(a);
            await _repo.AddAsync(b);
            return new BaseResponse<bool>("Swap successful", StatusCodeEnum.OK_200, true);
        }
    }
}
