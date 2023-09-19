using PowerPlanApi.Models;

namespace PowerPlanApi.Services
{
    public class ProductionPlanService : IProductionPlanService
    {

        public IList<PayLoadResponse> CalculateProductionPlan(PayLoadRequest payLoadRequest)
        {
            if (payLoadRequest == null)
            {
                throw new ArgumentNullException("The request cannot be null.");
            }
            if (payLoadRequest.PowerPlants == null || payLoadRequest.Fuels == null)
            {
                throw new ArgumentNullException("PowerPlants and Fuels must be provided.");
            }
            IList<PayLoadResponse> payLoadResponse = new List<PayLoadResponse>();
            decimal totalPower = 0;
            foreach (var PowerPlant in payLoadRequest.PowerPlants)
            {
                PowerPlant.CostPerMWh = calculateCost(PowerPlant, payLoadRequest.Fuels);
                payLoadResponse.Add(new PayLoadResponse { Name = PowerPlant.Name, Power = 0 });
            }
            var orderedPowerPlants = payLoadRequest.PowerPlants
                .Where(b => b.CostPerMWh != null)
                .OrderBy(b => b.CostPerMWh);
            foreach (var PowerPlant in orderedPowerPlants)
            {
                if (totalPower == payLoadRequest.Load)
                    break;
                var powerPlantPower = calculatePower(PowerPlant, payLoadRequest.Fuels["wind(%)"]);
                var hasAtteintlimit = powerPlantPower + totalPower > payLoadRequest.Load;
                payLoadResponse.First(a => a.Name == PowerPlant.Name).Power = hasAtteintlimit
                    ? payLoadRequest.Load - totalPower
                    : powerPlantPower;
                totalPower = hasAtteintlimit
                   ? payLoadRequest.Load
                   : powerPlantPower + totalPower;
            }
            return payLoadResponse;

        }

        private decimal calculatePower(PowerPlant powerPlant, decimal windPercent)
        {
            if (windPercent < 0 || windPercent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(windPercent), "Wind percentage must be between 0 and 100.");
            }

            return powerPlant.Type == "windturbine" ? Math.Round(powerPlant.PMax * windPercent / 100, 1) : powerPlant.PMax;
        }

        private decimal? calculateCost(PowerPlant powerPlant, Dictionary<string, decimal> fuels)
        {
            if (powerPlant.Efficiency == 0)
                return null;
            switch (powerPlant.Type)
            {
                case "windturbine": { return 0; }
                case "gasfired": { return fuels["gas(euro/MWh)"] / powerPlant.Efficiency; }
                case "turbojet": { return fuels["kerosine(euro/MWh)"] / powerPlant.Efficiency; }
                default: throw new ArgumentOutOfRangeException("invalid plant type");
            }
        }
    }
}
