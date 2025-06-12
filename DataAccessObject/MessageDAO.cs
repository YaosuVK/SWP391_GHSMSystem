using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class MessageDAO : BaseDAO<Message>
    {
        private readonly GHSMContext _context;

        public MessageDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }
    }
}
