using PowerPlanApi.Models;

namespace PowerPlanApi.Services
{
    public interface IProductionPlanService
    {
        IList<PayLoadResponse> CalculateProductionPlan(PayLoadRequest payLoad);
    }
}
