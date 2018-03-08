<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <button v-on:click="allAccounts">allAccounts</button>
    <button v-on:click="readFromContract">read</button>
    <button v-on:click="writeToContract">write</button>
    <input v-model="address"  type="text"/>
    <button v-on:click="getBalance">getBalance</button>
  </div>
</template>

<script>

      import Web3 from 'web3'
      import ContractResources from '../services/ContractResource';
      let	contractInstance;
      let baseUrl = "http://localhost:60099/api/contract"
      let contractResources = new ContractResources()
      //let provider = new Web3(new Web3.providers.HttpProvider("http://localhost:9545"));
     // var event = contractInstance.newValue()

      // watch for changes
     // event.watch(function(error, result){
     //   if (!error)
     //       {
     //         console.log(result.args.oldValue.toNumber() + "," + result.args.newValue.toNumber());
     //       }
     //  });

export default {
  name: 'HelloWorld',
  data () {
    return {  
      msg: 'Welcome to HaveFun Funds Dapp',
      address: "0x5aeda56215b167893e80b4fe645ba6d5bab767de"
    }
  },
    methods: {
    init () { 
     this.$http.get(baseUrl + '/metadata')
        .then(response => {
          let contractMetadata = response.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          contractInstance = contract.at(contractMetadata.address);
        });
    },
    allAccounts(){
        provider.eth.getAccounts((error, accounts) => console.log(accounts))
    },
    readFromContract(){
      contractInstance.hasOwnerRights.call(function(err, res) {
        if(!err){
          console.log(res);
        } else {
          console.log("Something went horribly wrong");
        }
      });
    },
    writeToContract(){
      contractInstance.donateForCampaign('0x6330a553fc93768f612722bb8c2ec78ac90b3bbc',{from: web3.eth.accounts[0], gas: 3000000, value: 10000000000000000000}, function(err, res){
        if(!err){
          console.log("Success! Transaction hash: " + res.valueOf());
        } else {
          console.log("Something went wrong. Are you sure that you are the current owner?");
        }
      });
    },
    getBalance(){
        provider.eth.getBalance(this.address, 
            (error, balance) => console.log(balance.toNumber()))
    }
  },
  created () {
    this.init()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal; 
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
