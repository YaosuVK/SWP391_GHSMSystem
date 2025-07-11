﻿using BusinessObject.Model;
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

        public async Task<IEnumerable<ConsultantSlot>> GetAllConsultantSlotAsync()
        {
            return await _context.ConsultantSlots
                .Include(cs => cs.Slot)
                .Include(cs => cs.Consultant)
            .ToListAsync();
        }

        /// Lấy một ConsultantSlot theo ConsultantID và SlotID

        public async Task<ConsultantSlot> GetByIdAsync(string consultantId, int slotId)
        {
            return await _context.ConsultantSlots
                                 .Include(cs => cs.Slot)
                                 .Include(cs => cs.Consultant)
                                 .FirstOrDefaultAsync(cs => cs.ConsultantID == consultantId && cs.SlotID == slotId);
        }


        /// Lấy booking nếu consultant đã đăng ký slot cụ thể

        public async Task<ConsultantSlot> GetByConsultantAndSlotAsync(string consultantId, int slotId)
        {
            return await _context.ConsultantSlots
                                .Include(cs => cs.Slot)
                                .Include(cs => cs.Consultant)
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
                                .Include(cs => cs.Consultant)
                                .Where(cs => cs.ConsultantID == consultantId);


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ConsultantSlot>> SearchConsultantSlotsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.ConsultantSlots
                    .Include(cs => cs.Slot)
                    .Include(cs => cs.Consultant)
                    .ToListAsync();
            }

            var lowerCaseKeyword = keyword.ToLower();
            return await _context.ConsultantSlots
                .Include(cs => cs.Slot)
                .Include(cs => cs.Consultant)
                .Where(cs =>
                    cs.ConsultantID.ToLower().Contains(lowerCaseKeyword) ||
                    cs.SlotID.ToString().Contains(lowerCaseKeyword) ||
                    cs.MaxAppointment.ToString().Contains(lowerCaseKeyword) ||
                    cs.AssignedDate.ToString().Contains(lowerCaseKeyword))
                .ToListAsync();
        }
    }
}
