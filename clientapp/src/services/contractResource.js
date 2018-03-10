
export default class ContractService {
  baseUrl = "http://localhost:60099/api/contract"
  contractInstance;
  httpProvider;
  contractOwner;
  constructor(httpProvider) {
    this.httpProvider = httpProvider;
     this.httpProvider.get(this.baseUrl + '/metadata')
        .then(response => {
          let contractMetadata = response.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          this.contractInstance = contract.at(contractMetadata.address);
          this.contractOwner = contractMetadata.owner;
          console.log(this.contractOwner)
       });
  }

  isOwner(callback) {
    this.contractInstance.hasOwnerRights(function(err, succ){
      callback(err, succ)
    })
  }

  donate(callback) {
    console.log("Donate from: " + web3.eth.accounts[0]);
    this.contractInstance.donate({from: web3.eth.accounts[0], gas: 3000000, value: 50000000000000000}, function(err, succ){
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
    this.contractInstance.getBalance({from:  this.contractOwner},function(err, succ) {
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

  loadGivers(address, callback){
    this.httpProvider.get(this.baseUrl + '/givers/' + address)
    .then(response => {
      callback(response.body);
    });
  }

  createCampaign(address, amountInEther, callback){
    this.contractInstance.startCampaign(address,amountInEther, {from: web3.eth.accounts[0], gas: 3000000}, function(err, succ){
      callback(err, succ)
  })
  }
}