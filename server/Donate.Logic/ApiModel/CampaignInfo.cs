using Newtonsoft.Json;

namespace Donate.Logic.ApiModel
{
    public class CampaignInfo
    {
        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("fundsNeeded")]
        public decimal FundsNeeded { get; set; }

        [JsonProperty("collected")]
        public decimal Collected { get; set; }

        [JsonProperty("giversCount")]
        public long GiversCount { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        [JsonProperty("fundsProgress")]
        public decimal FundsProgress { get; set; }

        [JsonProperty("collectPeriod")]
        public double CollectPeriod { get; set; }
    }
}
