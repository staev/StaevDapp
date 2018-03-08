using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace Donate.Logic
{
    [FunctionOutput]
    public class GeneralInfo
    {
        [Parameter("uint256", "totalFunds", 1)]
        public BigInteger TotalFunds { get; set; }

        [Parameter("uint256", "allCampaings", 2)]
        public BigInteger AllCampaings { get; set; }
    }
}
