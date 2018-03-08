using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace Donate.Logic
{
    [FunctionOutput]
    public class CampaignInfo
    {
        [Parameter("address", "campaignOwner", 1)]
        public string CampaignOwner { get; set; }

        [Parameter("uint256", "totalNeeded", 2)]
        public BigInteger TotalNeeded { get; set; }

        [Parameter("uint256", "collectedUntilNow", 3)]
        public BigInteger CollectedUntilNow { get; set; }

        [Parameter("uint256", "giversCount", 4)]
        public long GiversCount { get; set; }

        [Parameter("bool", "isActive", 5)]
        public bool IsActive { get; set; }
    }
}
