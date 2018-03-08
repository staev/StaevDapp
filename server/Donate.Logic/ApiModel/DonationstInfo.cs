using Newtonsoft.Json;
using System.Collections.Generic;

namespace Donate.Logic.ApiModel
{
    public class DonationstInfo
    {
        [JsonProperty("totalFunds")]
        public decimal TotalFunds { get; set; }

        [JsonProperty("allCampaings")]
        public long AllCampaings { get; set; }

        [JsonProperty("campaigns")]
        public List<CampaignInfo> Campaigns { get; set; }

        public DonationstInfo()
        {
            Campaigns = new List<CampaignInfo>();
        }

    }
}
