using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace Donate.Logic
{
    public interface IDonateContract
    {
        List<CampaignInfo> GetCampaigns();
    }

    public class DonateContract : IDonateContract
    {
        private HexBigInteger _defaultGas = new HexBigInteger(3000000);
        private HexBigInteger _zero = new HexBigInteger(0);

        private string PrivateKey { get; set; }
        private string NetworkUrl { get; set; }

        private string ContractAddress { get; set; }
        private Web3 Web3 { get; set; }
        private Account Account { get; set; }

        private Contract GetContract()
        {
            var info = GetContractInfo();
            return Web3.Eth.GetContract(info.GetAbi(), ContractAddress);
        }

        public DonateContract(string privateKey, string networkUrl)
        {
            PrivateKey = privateKey;
            NetworkUrl = networkUrl;

            Account = new Account(PrivateKey);
            Web3 = new Web3(Account, NetworkUrl);
            Web3.TransactionManager.DefaultGas = BigInteger.Parse("3000000");
            Web3.TransactionManager.DefaultGasPrice = Transaction.DEFAULT_GAS_PRICE;

            DeployContract();
        }

        private void DeployContract()
        {
            if (string.IsNullOrEmpty(ContractAddress))
            {
                var contract = GetContractInfo();
                var senderAddress = Account.Address;
                var transactionHash = Web3.Eth.DeployContract.SendRequestAsync(contract.GetAbi(), contract.ByteCode, senderAddress, _defaultGas, new object[] { }).GetAwaiter().GetResult();

                var receipt = Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                while (receipt == null)
                {
                    Thread.Sleep(1000);
                    receipt = Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                }

                ContractAddress = receipt.ContractAddress;
            }
        }

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText(@"..\..\truffle\build\contracts\DonateContract.json");
            return JsonConvert.DeserializeObject<ContractMetaInfo>(json);
        }

        public List<CampaignInfo> GetCampaigns()
        {
            List<CampaignInfo> campaigns = new List<CampaignInfo>();

            var contract = GetContract();

            GeneralInfo generalInfo = contract.GetFunction("allCampaignsInfo").CallDeserializingToObjectAsync<GeneralInfo>().GetAwaiter().GetResult();

            for (int i = 0; i < generalInfo.AllCampaings; i++)
            {
                CampaignInfo campaignInfo = contract.GetFunction("campaignDetails").CallDeserializingToObjectAsync<CampaignInfo>(ContractAddress, _defaultGas, _zero, i).GetAwaiter().GetResult();
            }

            return campaigns;
        }
    }
}
