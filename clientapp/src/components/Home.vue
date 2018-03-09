<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    Please donate: <input v-model="amount" />
    <div>
      <div  v-for="c in campaigns" v-bind:key="c.owner">
        <p>
          {{ c.owner }}
        </p>
         <p>
         Sum needed: {{ c.fundsNeeded }}
        </p>
         <p>
         Collected: {{ c.collected }}
        </p>
          <p>
         Donations: {{ c.giversCount }}
        </p>
        <p>
         Started: {{ c.startDate }}
        </p>
        <p v-if="!c.isActive">
         Collected at: {{ c.endDate }}
        </p>
        <button v-on:click="donate(c.owner)">Donate</button>
        <a href=""Details></a>
      </div>
    </div>
  </div>
</template>

<script>

      import Web3 from 'web3'
      import ContractResources from '../services/ContractResource';
      let	contractInstance;
      let baseUrl = "http://localhost:60099/api/contract"
      let contractResources = new ContractResources()

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
      address: "0x5aeda56215b167893e80b4fe645ba6d5bab767de",
      campaigns :[],
      amount : 1
    }
  },
    methods: {
    init () { 
     this.$http.get(baseUrl + '/metadata')
        .then(response => {
          let contractMetadata = response.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          contractInstance = contract.at(contractMetadata.address);
           this.$http.get(baseUrl + '/info')
            .then(response => {
                this.campaigns = response.body.campaigns;
                console.log(response.body);
            });
        });
    },
    allAccounts(){
        web3.eth.getAccounts((error, accounts) => console.log(accounts))
    },
    readFromContract(){
      contractInstance.hasOwnerRights(function(err, res) {
        if(!err){
          console.log(res);
        } else {
          console.log("Something went horribly wrong");
        }
      });
    },
    donate(contractOwner){
      let weiiAmount = web3.toWei(this.amount)
      contractInstance.donateForCampaign(contractOwner, {from: web3.eth.accounts[0], gas: 3000000, value: weiiAmount}, function(err, res){
        if(!err){
          console.log("Success! Transaction hash: " + res.valueOf());
        } else {
          console.log("Something went wrong. Are you sure that you are the current owner?");
        }
      });
    },
    getBalance(){
        web3.eth.getBalance(this.address, 
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
