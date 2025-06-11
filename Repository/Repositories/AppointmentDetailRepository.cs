using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AppointmentDetailRepository : BaseRepository<AppointmentDetail>, IAppointmentDetailRepository
    {
        private readonly AppointmentDetailDAO _appointmentDetailDao;
        public AppointmentDetailRepository(AppointmentDetailDAO appointmentDetailDao) : base(appointmentDetailDao)
        {
            _appointmentDetailDao = appointmentDetailDao;
        }

        public async Task<List<AppointmentDetail>> DeleteAppointmentDetailAsync(List<AppointmentDetail> appointmentDetails)
        {
            return await _appointmentDetailDao.DeleteRange(appointmentDetails);
        }

        public async Task<List<AppointmentDetail>> GetAppointmentDetailsToRemoveAsync(int appointmentId, List<int> updatedDetailIds)
        {
            return await _appointmentDetailDao.GetAppointmentDetailsToRemoveAsync(appointmentId, updatedDetailIds);
        }

        public async Task<List<(string serviceName, int count)>> GetServiceUsageStatsAsync()
        {
            return await _appointmentDetailDao.GetServiceUsageStatsAsync();
        }
    }
}
