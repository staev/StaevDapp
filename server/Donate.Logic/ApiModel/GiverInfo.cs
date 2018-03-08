using Newtonsoft.Json;
using System;

namespace Donate.Logic.ApiModel
{
    public class GiverInfo
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
