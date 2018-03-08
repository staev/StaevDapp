using Donate.Logic;
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

        [HttpGet("all-campaings")]
        public void AllCampaigns()
        {
           var data =  DonateContract.GetCampaigns();
        }
    }
}
