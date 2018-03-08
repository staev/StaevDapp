using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Numerics;

namespace Donate.Logic
{
    [FunctionOutput]
    public class GiverInfo
    {
        [Parameter("address", "giver", 1)]
        public string Address { get; set; }

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }

        [Parameter("uint256", "date", 3)]
        public long Date { get; set; }
    }
}
