using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace Donate.Logic
{
    public interface IDonateContract
    {
        ApiModel.DonationstInfo DonationsInfo();
        List<ApiModel.GiverInfo> GiversForCampaing(string address, int count);
        ApiModel.ContractMetadata GetMetadata();
    }

    public class DonateContract : IDonateContract
    {
        private HexBigInteger defaultGas = new HexBigInteger(3000000);
        private HexBigInteger zero = new HexBigInteger(0);

        private Web3 Web3 { get; set; }
        private Account Account { get; set; }
        private ContractConfig Config { get; set; }
        private string ContractAddress { get; set; }
        private string Owner { get; set; }

        private Contract contract;
        private Contract Contract
        {
            get
            {
                if (contract == null)
                {
                    var info = GetContractInfo();
                    contract = Web3.Eth.GetContract(info.GetAbi(), ContractAddress);
                }
                return contract;
            }
        }

        public DonateContract(ContractConfig config)
        {
            Config = config;

            Account = new Account(Config.PrivateKey);
            Owner = Account.Address;

            Web3 = new Web3(Account, config.NetworkUrl);
            Web3.TransactionManager.DefaultGas = BigInteger.Parse("3000000");
            Web3.TransactionManager.DefaultGasPrice = Transaction.DEFAULT_GAS_PRICE;

            DeployContract();
            InitCampaigns();
        }

        private void DeployContract()
        {
            if (string.IsNullOrEmpty(ContractAddress))
            {
                var contract = GetContractInfo();
                var senderAddress = Account.Address;
                var transactionHash = Web3.Eth.DeployContract.SendRequestAsync(contract.GetAbi(), contract.ByteCode, senderAddress, defaultGas, new object[] { }).GetAwaiter().GetResult();

                var receipt = Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                while (receipt == null)
                {
                    Thread.Sleep(1000);
                    receipt = Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                }

                ContractAddress = receipt.ContractAddress;
            }
        }

        private HexBigInteger EtherToWeii(int amount)
        {
            BigInteger weii = Web3.Convert.ToWei(amount);
            return new HexBigInteger(weii);
        }

        private void InitCampaigns()
        {
            var startCampaignFunc = Contract.GetFunction("startCampaign");
            var donateForCampaign = Contract.GetFunction("donateForCampaign");

            if (!string.IsNullOrEmpty(Config.Campaing1Owner))
                startCampaignFunc.SendTransactionAsync(Owner, defaultGas, zero, Config.Campaing1Owner, 5).GetAwaiter().GetResult();

            donateForCampaign.SendTransactionAsync(Owner, defaultGas, EtherToWeii(1), Config.Campaing1Owner).GetAwaiter().GetResult();
            donateForCampaign.SendTransactionAsync(Owner, defaultGas, EtherToWeii(2), Config.Campaing1Owner).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(Config.Campaing2Owner))
                startCampaignFunc.SendTransactionAsync(Owner, defaultGas, zero, Config.Campaing2Owner, 3).GetAwaiter().GetResult();
        }

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText(@"..\..\truffle\build\contracts\DonateContract.json");
            return JsonConvert.DeserializeObject<ContractMetaInfo>(json);
        }

        private decimal WeiiToEther(BigInteger value)
        {
            decimal ethers = Nethereum.Util.UnitConversion.Convert.FromWei(value);
            return ethers;
        }

        private string ToDate(long value)
        {
            string date = new DateTime(1970, 1, 1).AddSeconds(value).ToLocalTime().ToString("M/d/yyyy hh:mm");
            return date;
        }

        public ApiModel.DonationstInfo DonationsInfo()
        {
            ApiModel.DonationstInfo info = new ApiModel.DonationstInfo();

            GeneralInfo generalInfo = Contract.GetFunction("allCampaignsInfo").CallDeserializingToObjectAsync<GeneralInfo>().
                GetAwaiter().GetResult();

            info.TotalFunds = WeiiToEther(generalInfo.TotalFunds);
            info.AllCampaings = generalInfo.AllCampaings;

            for (long i = 0; i < generalInfo.AllCampaings; i++)
            {
                CampaignInfo cInfo = contract.GetFunction("campaignDetails").CallDeserializingToObjectAsync<CampaignInfo>(ContractAddress, defaultGas, zero, i).GetAwaiter().GetResult();

                ApiModel.CampaignInfo campaignInfo = new ApiModel.CampaignInfo();
                campaignInfo.Owner = cInfo.CampaignOwner;
                campaignInfo.Collected = WeiiToEther(cInfo.CollectedUntilNow);
                campaignInfo.FundsNeeded = WeiiToEther(cInfo.TotalNeeded);
                campaignInfo.GiversCount = cInfo.GiversCount;
                campaignInfo.IsActive = cInfo.IsActive;
                campaignInfo.StartDate = ToDate(cInfo.StartDate);
                if (cInfo.EndDate > 0)
                    campaignInfo.EndDate = ToDate(cInfo.EndDate);

                info.Campaigns.Add(campaignInfo);
            }

            return info;
        }

        public List<ApiModel.GiverInfo> GiversForCampaing(string address, int count)
        {
            List<ApiModel.GiverInfo> givers = new List<ApiModel.GiverInfo>();
            for (int i = 0; i < count; i++)
            {
                GiverInfo gInfo = contract.GetFunction("giverDetails").CallDeserializingToObjectAsync<GiverInfo>(ContractAddress, defaultGas, zero, address, i).GetAwaiter().GetResult();

                ApiModel.GiverInfo giverInfo = new ApiModel.GiverInfo();
                giverInfo.Address = gInfo.Address;
                giverInfo.Amount = WeiiToEther(gInfo.Amount);
                giverInfo.Date = ToDate(gInfo.Date);

                givers.Add(giverInfo);
            }

            return givers;
        }

        public ApiModel.ContractMetadata GetMetadata()
        {
            ApiModel.ContractMetadata metaData = new ApiModel.ContractMetadata();
            metaData.Address = ContractAddress;
            metaData.Abi = JsonConvert.SerializeObject(GetContractInfo().Abi);

            return metaData;
        }

    }
}
