
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

  isOwner(callback) {
    this.contractInstance.hasOwnerRights(function(err, succ){
      callback(err, succ)
    })
  }

  donate(callback) {
    console.log("Donate from: " + web3.eth.accounts[0]);
    this.contractInstance.donate({from: web3.eth.accounts[0], gas: 3000000, value: 5000000000000000000}, function(err, succ){
      debugger
      callback(err, succ)
    })
  }

  getCampaignsInfo(callback){
    this.httpProvider.get(this.baseUrl + '/info')
      .then(response => {
        callback(response.body)
    });
  }

  getBalance(callback){
    this.contractInstance.getBalance({from: '0x627306090abab3a6e1400e9345bc60c78a8bef57'},function(err, succ) {
      callback(err, succ)
    })
  }

  donateForCampaign (address, amount, callback){
    let weiiAmount = web3.toWei(amount, 'ether')
    console.log(weiiAmount)
    this.contractInstance.donateForCampaign(address, {from: web3.eth.accounts[0], gas: 3000000, value: weiiAmount}, function(err, succ){
        callback(err, succ)
    })
  }
}