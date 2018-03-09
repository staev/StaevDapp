
export default class ContractService {
  baseUrl = "http://localhost:60099/api/contract"
  contractInstance;
  httpProvider;
  constructor(httpProvider) {
    this.httpProvider = httpProvider;
    this.httpProvider.get(this.baseUrl + '/metadata')
        .then(response => {
          let contractMetadata = response.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          this.contractInstance = contract.at(contractMetadata.address);
          console.log(this.contractInstance)
       });
  }

  isOwner() {
    this.contractInstance.hasOwnerRights(function(err, succ){
      callback(err, succ)
    })
  }

  getCampaignsInfo(callback){
    this.httpProvider.get(this.baseUrl + '/info')
      .then(response => {
        for (let index = 0; index < response.body.campaigns.length; index++) {
          
        }
        callback(response.body)
    });
  }

  donateForCampaign (address, amount, callback){
    let weiiAmount = web3.toWei(amount, 'ether')
    console.log(weiiAmount)
    this.contractInstance.donateForCampaign(address, {from: web3.eth.accounts[0], gas: 3000000, value: weiiAmount}, function(err, succ){
        callback(err, succ)
    })
  }
}