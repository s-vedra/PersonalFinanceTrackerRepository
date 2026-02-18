using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace PersonalFinanceApplication_Services.EventServices.SalarySchedulerEvent
{
    public class HangfireAuthenticationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
