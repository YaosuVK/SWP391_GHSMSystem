using BusinessObject.Model;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TreatmentOutcomeRepository : BaseRepository<TreatmentOutcome>, ITreatmentOutcomeRepository
    {
        private readonly TreatmentOutcomeDAO _treatmentOutcomeDAO;

        public TreatmentOutcomeRepository(TreatmentOutcomeDAO treatmentOutcomeDAO) : base(treatmentOutcomeDAO)
        {
            _treatmentOutcomeDAO = treatmentOutcomeDAO;
        }

        public async Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByCustomerIdAsync(string customerId)
        {
            return await _treatmentOutcomeDAO.GetQueryable()
                .Where(to => to.CustomerID == customerId)
                .Include(to => to.Customer)
                .Include(to => to.Consultant)
                .Include(to => to.Appointment)
                .Include(to => to.LabTests)
                .OrderByDescending(to => to.CreateAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByConsultantIdAsync(string consultantId)
        {
            return await _treatmentOutcomeDAO.GetQueryable()
                .Where(to => to.ConsultantID == consultantId)
                .Include(to => to.Customer)
                .Include(to => to.Consultant)
                .Include(to => to.Appointment)
                .Include(to => to.LabTests)
                .OrderByDescending(to => to.CreateAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentOutcome>> GetTreatmentOutcomesByAppointmentIdAsync(int appointmentId)
        {
            return await _treatmentOutcomeDAO.GetQueryable()
                .Where(to => to.AppointmentID == appointmentId)
                .Include(to => to.Customer)
                .Include(to => to.Consultant)
                .Include(to => to.Appointment)
                .Include(to => to.LabTests)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentOutcome>> SearchTreatmentOutcomesAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _treatmentOutcomeDAO.GetQueryable()
                .Include(to => to.Customer)
                .Include(to => to.Consultant)
                .Include(to => to.Appointment)
                .Include(to => to.LabTests)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(to => 
                    to.Diagnosis.Contains(search) ||
                    to.TreatmentPlan.Contains(search) ||
                    to.Prescription.Contains(search) ||
                    to.Recommendation.Contains(search) ||
                    to.Customer.FullName.Contains(search) ||
                    to.Consultant.FullName.Contains(search));
            }

            return await query
                .OrderByDescending(to => to.CreateAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TreatmentOutcome> GetTreatmentOutcomeWithDetailsAsync(int id)
        {
            return await _treatmentOutcomeDAO.GetQueryable()
                .Where(to => to.TreatmentID == id)
                .Include(to => to.Customer)
                .Include(to => to.Consultant)
                .Include(to => to.Appointment)
                .Include(to => to.LabTests)
                .FirstOrDefaultAsync();
        }
    }
} 