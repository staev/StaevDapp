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

  debugger
      import Web3 from 'web3'
      import ContractResources from '../services/ContractResource';

      let contractResources = new ContractResources()
     
      //let contractResources = new ContractResource()
      let contractAbi = contractResources.getContractAbi();
      let contractAddress=contractResources.getContractAddress();
      let provider = new Web3(new Web3.providers.HttpProvider("http://localhost:9545"));

      let contract = provider.eth.contract(contractAbi);
      let	contractInstance = contract.at(contractAddress);

      var event = contractInstance.newValue()

      // watch for changes
      event.watch(function(error, result){
        if (!error)
            {
              console.log(result.args.oldValue.toNumber() + "," + result.args.newValue.toNumber());
            }
      });

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
    
        
    },
    allAccounts(){
        provider.eth.getAccounts((error, accounts) => console.log(accounts))
    },
    readFromContract(){
      contractInstance.get.call({"from": this.address }, function(err, res) {
        if(!err){
          console.log(res.toNumber());
        } else {
          console.log("Something went horribly wrong");
        }
      });
    },
    writeToContract(){
      contractInstance.increment({"from": this.address}, function(err, res){
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
