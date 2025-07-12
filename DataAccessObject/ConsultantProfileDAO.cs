using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ConsultantProfileDAO : BaseDAO<ConsultantProfile>
    {
        private readonly GHSMContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ConsultantProfileDAO(GHSMContext context, UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ConsultantProfile?> GetConsultantProfileByAccountID(string accountID)
        {
            return await _context.ConsultantProfiles
                .Include(x => x.Account)
                .ThenInclude(x => x.ConsultantSlots)
                .ThenInclude(x => x.Slot)
                .FirstOrDefaultAsync(x => x.AccountID == accountID);
        }

        public async Task<ConsultantProfile?> GetConsultantProfileByID(int? consultantProfileID)
        {
            return await _context.ConsultantProfiles
                .Include(x => x.Account)
                .ThenInclude(x => x.ConsultantSlots)
                .ThenInclude(x => x.Slot)
                .FirstOrDefaultAsync(x => x.ConsultantProfileID == consultantProfileID);
        }

        public async Task<IEnumerable<ConsultantProfile?>> GetAllConsultantProfile()
        {
            return await _context.ConsultantProfiles
                .Include(x => x.Account)
                .ThenInclude(x => x.ConsultantSlots)
                .ThenInclude(x => x.Slot)
                .ToListAsync();
        }

    }
}
