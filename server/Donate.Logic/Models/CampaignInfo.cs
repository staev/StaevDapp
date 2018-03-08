using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Donate.Logic
{
    public class CampaignInfo
    {
        [Parameter("uint256", "totalNeeded", 1)]
        public int TotalNeeded { get; set; }

        [Parameter("uint256", "collectedUntilNow", 2)]
        public int CollectedUntilNow { get; set; }

        [Parameter("uint256", "giversCount", 3)]
        public int GiversCount { get; set; }

        [Parameter("bool", "isActive", 4)]
        public int IsActive { get; set; }
    }
}
