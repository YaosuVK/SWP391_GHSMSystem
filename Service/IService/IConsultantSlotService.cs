using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IConsultantSlotService
    {
        Task<BaseResponse<ConsultantSlot>> RegisterAsync(string consultantId, int slotId, int maxAppointment);
        Task<BaseResponse<bool>> SwapAsync(string consultantIdA, int slotIdA, string consultantIdB, int slotIdB);
        Task<BaseResponse<IEnumerable<ConsultantSlot>>> GetRegisteredSlotsAsync(string consultantId);
    }
}
