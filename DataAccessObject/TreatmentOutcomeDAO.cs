using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class TreatmentOutcomeDAO : BaseDAO<TreatmentOutcome>
    {
        private readonly GHSMContext _context;

        public TreatmentOutcomeDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }
    }
}
