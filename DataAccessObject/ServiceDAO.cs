using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ServiceDAO : BaseDAO<Service>
    {
        private readonly GHSMContext _context;

        public ServiceDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }
    }
}
