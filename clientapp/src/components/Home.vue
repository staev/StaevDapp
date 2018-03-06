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
      //import ContractResource from '../services/ContractResource.js';
      //let contractResources = new ContractResource()
      let contractAbi = [
    {
      "inputs": [],
      "payable": false,
      "stateMutability": "nonpayable",
      "type": "constructor"
    },
    {
      "constant": true,
      "inputs": [],
      "name": "get",
      "outputs": [
        {
          "name": "",
          "type": "uint256"
        }
      ],
      "payable": false,
      "stateMutability": "view",
      "type": "function"
    },
    {
      "constant": false,
      "inputs": [],
      "name": "increment",
      "outputs": [],
      "payable": false,
      "stateMutability": "nonpayable",
      "type": "function"
    }
  ];
      let contractAddress="0x345ca3e014aaf5dca488057592ee47305d9b3e10";
      let provider = new Web3(new Web3.providers.HttpProvider("http://localhost:9545"));
     // provider.eth.getAccounts((error, accounts) => console.log(accounts))
      let contract = provider.eth.contract(contractAbi);
      let	contractInstance = contract.at(contractAddress);
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
