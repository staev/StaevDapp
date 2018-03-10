<template>
    <div class="container  text-center">
    
        <h2>Create new Campaign</h2>
        <div class="form-group" style="padding-left:290px">
        <label class="control-label " for="address" style="float:left;padding-left:10px">Address:</label>
        <div class="col-sm-8">
            <input type="address" class="form-control" id="address" placeholder="Enter address" v-model="address" name="address">
        </div>
        </div>
        <div class="form-group" style="padding-left:290px" >
        <label class="control-label " for="amount" style="float:left;padding-left:10px">Needed Amount:</label>
        <div class="col-sm-8">          
            <input type="text" class="form-control" id="amount" placeholder="Enter amount" v-model="amount" name="amount">
        </div>
        </div>
        <div class="form-group">        
        <div class=" col-sm-10" style="padding-left:205px">
            <button class="btn btn-lg btn-block btn-outline-primary" style="width:190px;margin:18px auto;" v-on:click="createCampaign">Create campaign</button>
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
        address: "",
        amount: 10
    }
  },
    methods: {
    createCampaign(){
        debugger
      this.contractService.createCampaign(this.address, this.amount, function(err, res){
        if(!err){
          console.log("Success! Transaction hash: " + res.valueOf());
        } else {
          console.log("Something went wrong. Are you sure that you are the current owner?");
        }
      });
    }
  },
  created () {
    this.contractService = new ContractService(this.$http)
  }
}
</script>

<style>
</style>

