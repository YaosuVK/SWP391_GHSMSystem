using BusinessObject.Model;
using BusinessObject.PaginatedLists;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class FeedBackDAO : BaseDAO<FeedBack>
    {
        private readonly GHSMContext _context;

        public FeedBackDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
