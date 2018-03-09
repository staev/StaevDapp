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

        [HttpGet("givers/{address}")]
        public List<ApiModels.GiverInfo> Info(string address)
        {
            List< ApiModels.GiverInfo> givers = DonateContract.GiversForCampaing(address);
            return givers;
        }

        [HttpGet("metadata")]
        public ContractMetadata Metadata()
        {
            ContractMetadata metadata = DonateContract.GetMetadata();
            return metadata;
        }
    }
}
