namespace PowerPlanApi.Models
{
    public class PowerPlant
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Efficiency { get; set; }

        public decimal PMin { get; set; }

        public decimal PMax { get; set; }
        public decimal? CostPerMWh { get; set; }
    }
}
