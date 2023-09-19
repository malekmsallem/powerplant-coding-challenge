namespace PowerPlanApi.Models
{
    public class PayLoadRequest
    {
        public decimal Load { get; set; }

        public Dictionary<string, decimal> Fuels { get; set; }

        public IList<PowerPlant> PowerPlants { get; set; }
    }
}
