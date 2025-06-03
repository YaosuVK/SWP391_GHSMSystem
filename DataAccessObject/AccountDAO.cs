using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AccountDAO : BaseDAO<Account>
    {
        private readonly GHSMContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountDAO(GHSMContext context, UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(int totalAccount, int managersAccount, int customersAccount, int staffsAccount, int consultantAccount)> GetTotalAccount()
        {
            var customerRole = await _roleManager.FindByNameAsync("Customer");
            var customersCount = await _userManager.GetUsersInRoleAsync(customerRole.Name);

            var consultantRole = await _roleManager.FindByNameAsync("Consultant");
            var consultantsCount = await _userManager.GetUsersInRoleAsync(consultantRole.Name);

            var managerRole = await _roleManager.FindByNameAsync("Manager");
            var managersCount = await _userManager.GetUsersInRoleAsync(managerRole.Name);

            var StaffRole = await _roleManager.FindByNameAsync("Staff");
            var staffsCount = await _userManager.GetUsersInRoleAsync(StaffRole.Name);



            int totalAccountsCount = customersCount.Count + managersCount.Count + staffsCount.Count + consultantsCount.Count;
            int managersAccount = managersCount.Count;
            int customersAccount = customersCount.Count;
            int staffsAccount = staffsCount.Count;
            int consultantsAccount = consultantsCount.Count;

            return (totalAccountsCount, managersAccount, customersAccount, staffsAccount, consultantsAccount);
        }
    }
}
