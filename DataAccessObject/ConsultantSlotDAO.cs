using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ConsultantSlotDAO : BaseDAO<ConsultantSlot>
    {
        private readonly GHSMContext _context;

        public ConsultantSlotDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ConsultantSlot> AddSlotAsync(ConsultantSlot entity)
        {
            await _context.ConsultantSlots.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        /// Lấy một ConsultantSlot theo ConsultantID và SlotID

        public async Task<ConsultantSlot> GetByIdAsync(string consultantId, int slotId)
        {
            return await _context.ConsultantSlots
                                 .Include(cs => cs.Slot)
                                 .FirstOrDefaultAsync(cs => cs.ConsultantID == consultantId && cs.SlotID == slotId);
        }


        /// Lấy booking nếu consultant đã đăng ký slot cụ thể

        public async Task<ConsultantSlot> GetByConsultantAndSlotAsync(string consultantId, int slotId)
        {
            return await _context.ConsultantSlots
                                 .FirstOrDefaultAsync(cs => cs.ConsultantID == consultantId
                                                         && cs.SlotID == slotId);
        }


        /// Xóa booking của consultant

        public async Task<bool> DeleteAsync(ConsultantSlot entity)
        {
            _context.ConsultantSlots.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        /// Lấy tất cả booking của một consultant

        public async Task<IEnumerable<ConsultantSlot>> GetByConsultantAsync(string consultantId)
        {
            var query = _context.ConsultantSlots
                                .Include(cs => cs.Slot)
                                .Where(cs => cs.ConsultantID == consultantId);


            return await query.ToListAsync();
        }

    }
}
