<template>
  <div class="hello">
    <h1>Campaign details</h1>
     Please donate: <input v-model="amount" />
     <button v-on:click="donate()">Donate</button>
  </div>
</template>

<script>
      let baseUrl = "http://localhost:60099/api/contract"
      import Web3 from 'web3'
      import ContractService from '../services/ContractResource';
export default {
  name: 'Details',
  data () {
    return {  
      givers :[],
      amount : 1,
      address : ""
    }
  },
    methods: {
    loadGivers () {
      this.$http.get(baseUrl + '/metadata')
        .then(response => {
          let contractMetadata = response.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          contractInstance = contract.at(contractMetadata.address);
      });

     this.$http.get(baseUrl + '/givers/' + this.$route.params.address)
        .then(response => {
          this.givers = response.body;
          console.log(this.givers)
      });
    },
    donate(contractOwner){
      console.log(this.amount)
      this.contractService.donateForCampaign(this.address, this.amount, function(err, res){
        if(!err){
          console.log("Success! Transaction hash: " + res.valueOf());
        } else {
          console.log("Something went wrong. Are you sure that you are the current owner?");
        }
      });
    }
  },
  created () {
    this.address = this.$route.params.address
    this.contractService = new ContractService(this.$http)
    this.loadGivers()
  }
}
</script>

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
