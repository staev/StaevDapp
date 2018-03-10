<template>
  <div class="container">
    <div v-if="isActive" class="form-group form-inline row" style="padding-left: 157px;" >
      <div class="col-10">
        <input class="col form-control" type="text" v-model="amount" placeholder="Please enter amount to donate" id="example-text-input" style="width:70%;padding: 0.575rem .75rem !important;">
        <button type="button" class="btn btn-lg btn-block btn-outline-primary" v-on:click="donate()" style="width:150px;margin-left:15px; display:inline;">Donate</button>
      </div>
    </div>
    <div v-else >
      <h3>Funds about this campaign are already collected.</h3>
    </div>
     <div class="card mb-4 box-shadow margin-fix">
    <div class="card-header">
      <h4 class="my-0 font-weight-normal col-xs-2">Donations history</h4>
    </div>
    <div class="card-body">
        <table class="table">
  <thead>
    <tr>
      <th scope="col">Address</th>
      <th scope="col">Date</th>
      <th scope="col">Amount</th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="g in givers"  v-bind:class="[g.isMax ? maxDonation : '']">
      <td>{{g.address}}</td>
      <td>{{g.date}}</td>
      <td>{{g.amount}}</td>
      <td><img v-if="g.isMax" src="static/img/like.png" width="28px"></td>
    </tr>
  </tbody>
</table>
    </div> 
 </div>
</div>
</template>

<script>
import ContractService from '../services/ContractResource';
export default {
  name: 'Details',
  data () {
    return {  
      givers :[],
      amount : 1,
      address : "",
      isActive: true,
      maxDonation : "maxDonation"
    }
  },
    methods: {
    loadGivers () {
      // this.$router.push({ path: '/home'});
       let self = this;
       this.contractService.loadGivers(this.$route.params.address, function(data){
          self.givers = data;
          console.log(self.givers)
       })
    },
    donate(contractOwner){
      this.contractService.donateForCampaign(this.address, this.amount, function(err, res){
        if(!err){
          console.log("Success! Transaction hash: " + res.valueOf());
        } else {
          console.log("Something went wrong. Are you sure that you are the current owner?");
        }
        this.loadGivers();
      });
    }
  },
  created () {
    this.address = this.$route.params.address
    this.isActive = this.$route.params.isActive
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

.maxDonation{
  background-color:#86ed9e;
}
</style>
