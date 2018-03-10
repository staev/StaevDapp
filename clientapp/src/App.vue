<template>
  <div id="app">
      <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom box-shadow">
      <img src="/static/img/image7.png" width="31px"/>
      <h5 class="my-0 mr-md-auto font-weight-normal">HaveFun Funds</h5>
      <span class="p-2 text-dark" href="#">Address:</span>
      <span class="p-2 text-dark" href="#">{{currentAccount}}</span>
       
      <a v-if="isCurrentOwner" class="btn btn-outline-primary" v-on:click="donateForOwner" href="#">New Campaign</a>
      <a v-else class="btn btn-outline-primary" v-on:click="donateForOwner" href="#">{{donateText}}</a>
    </div>
    <router-view/>
  </div>
</template>

<script>
  import ContractService from './services/ContractResource';
  let contractService;
export default {
  name: 'App',
   data () {
    return {  
      currentAccount: "Loading...",
      isCurrentOwner : false,
      donateText : "Donate 0.05 ethers"
    }
  },
    methods: {  
    init () { 
        var self = this;
        
        this.contractService = new ContractService(this.$http)
        setTimeout(() => {
           setInterval(function() {                                 
            if (web3.eth.defaultAccount !== self.currentAccount) {
              self.currentAccount = web3.eth.defaultAccount;     
              self.contractService.isOwner(function(err, succ){
               self.isCurrentOwner = succ;
              })
              console.log('changed');                     
            }
          }, 1000);
        }, 700);
       
    },
    donateForOwner(){
      var self = this;
      self.donateText = "Donating..."
     self.contractService.donate(function(err,succ){
       debugger;
       console.log(err);
       if(!err){
          self.donateText = "Thank you!"
       }
        setTimeout(function(){
               self.donateText = "Donate 0.05 ethers"
          },5000)
     })
    }
  },
  created () {
    this.init()
     var self = this;
    setInterval(() => {
       self.contractService.getBalance(function(err,succ){
         console.log("Balance: " +  succ);
       })
    }, 5000);
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 5px;
}
</style>
