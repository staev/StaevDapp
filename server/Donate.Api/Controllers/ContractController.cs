using Donate.Logic;
using Donate.Logic.ApiModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ApiModels = Donate.Logic.ApiModel;

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

        [HttpGet("givers/{address}/{count}")]
        public List<ApiModels.GiverInfo> Info(string address, int count)
        {
            List< ApiModels.GiverInfo> givers = DonateContract.GiversForCampaing(address, count);
            return givers;
        }
    }
}
