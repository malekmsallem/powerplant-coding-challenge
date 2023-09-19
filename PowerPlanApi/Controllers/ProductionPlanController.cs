
using Microsoft.AspNetCore.Mvc;
using PowerPlanApi.Models;
using PowerPlanApi.Services;

namespace PowerPlanApi.Controllers
{
    [ApiController]
    [Route("api/productionplan")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;

        public ProductionPlanController(IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost]
        public IList<PayLoadResponse> CalculateProductionPlan([FromBody] PayLoadRequest payload)
        {
            return _productionPlanService.CalculateProductionPlan(payload);
        }
    }
}
