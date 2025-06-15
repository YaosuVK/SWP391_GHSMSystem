using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ISlotService
    {
        Task<BaseResponse<IEnumerable<SlotForCustomer>>> GetAllAsync();
        Task<BaseResponse<List<Slot>>> AddAsync(CreateSlotRequest entity);
        Task<BaseResponse<Slot>> UpdateAsync(int slotID, UpdateSlotRequest entity);
    }
}
