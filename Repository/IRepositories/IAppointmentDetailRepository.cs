using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAppointmentDetailRepository : IBaseRepository<AppointmentDetail>
    {
        Task<List<AppointmentDetail>> DeleteAppointmentDetailAsync(List<AppointmentDetail> appointmentDetails);
        Task<List<AppointmentDetail>> GetAppointmentDetailsToRemoveAsync(int appointmentId, List<int> updatedDetailIds);
        Task<List<(string serviceName, int count)>> GetServiceUsageStatsAsync();
    }
}
