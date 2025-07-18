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
    public class ServiceDAO : BaseDAO<Services>
    {
        private readonly GHSMContext _context;

        public ServiceDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Services>> GetServicesByIdsAsync(List<int> servicesIds)
        {
            return await _context.Services
                .Include(h => h.ImageServices)
                .Include(h => h.Manager)
                .Include(h => h.Category)
                .Where(h => servicesIds.Contains(h.ServicesID)).ToListAsync();
        }

        public async Task<IEnumerable<Services>> GetAllServiceAsync()
        {
            return await _context.Services
                .Include(h => h.ImageServices)
                .Include(h => h.Manager)
                .Include(h => h.Category)
                .ToListAsync();
        }

        public async Task<Services?> GetServiceByID(int? serviceID)
        {
            return await _context.Services
                .Include(x => x.Category)
                .Include(x => x.Manager)
                .Include(h => h.ImageServices)
                .FirstOrDefaultAsync(x => x.ServicesID == serviceID);
        }
    }
}
