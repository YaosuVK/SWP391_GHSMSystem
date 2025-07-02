using Hangfire.Dashboard;

namespace GHSMSystem.HangFireSetUp
{
    public class DashboardNoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
