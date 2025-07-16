using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ITreatmentOutcomeRepository : IBaseRepository<TreatmentOutcome>
    {
        Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByCustomerIdAsync(string customerId);
        Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByConsultantIdAsync(string consultantId);
        Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<TreatmentOutcome>> SearchTreatmentOutcomesAsync(string? search, int pageIndex, int pageSize);
        Task<TreatmentOutcome> GetTreatmentOutcomeWithDetailsAsync(int id);
        Task<TreatmentOutcome?> GetTreatmenOutComeByAppointmentIdAsync(int appointmentId);
        Task<TreatmentOutcome?> GetTreatmenOutComeByTreatmentIdAsync(int treatementID);
    }
} 