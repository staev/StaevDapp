using Donate.Logic;
using Donate.Logic.ApiModel;
using Microsoft.AspNetCore.Mvc;

namespace Donate.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContractController : Controller
    {
        public IDonateContract DonateContract { get; set; }

        public ContractController(IDonateContract donateContract)
        {
            DonateContract = donateContract;
        }

        [HttpGet("info")]
        public DonationstInfo Info()
        {
            DonationstInfo info = DonateContract.DonationsInfo();
            return info;
        }
    }
}
