<template>
  <div class="container">
 <div class="card mb-4 box-shadow margin-fix">
    <div class="card-header">
      <h4 class="my-0 font-weight-normal col-xs-2">Wellcome to HaveFun Funds</h4>
    </div>
      <div class="card-body">We have reached {{totalDonations}} campaings, collecting {{totalCollected}} ethers helping people over the world. Be happy and enjoy donating.</div> 
 </div>
    
   <div class="card-deck mb-3 text-center">
     <div v-for="c in campaigns" v-bind:key="c.owner">
        <div class="card mb-4 box-shadow">
          <div class="card-header">
            <h4 class="my-0 font-weight-normal col-xs-2">{{ c.owner }}</h4>
          </div>
            <img v-if="!c.isActive" style="position:absolute; left:20px; top:85px;" src="static/img/done.png" width="64px" />
            <img v-else style="position:absolute; left:20px; top:80px;" src="static/img/inprogress.png" width="130px" />
          
          <div class="card-body">
            <h1 class="list-unstyled mt-3 mb-4">{{ c.collected }} <small class="text-muted">from </small>{{ c.fundsNeeded }}</h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>From {{ c.startDate}}</li>
              <li>Total donations: {{ c.giversCount }}</li>
            </ul>
            <ul v-if="c.isActive" class="list-unstyled mt-3 mb-4">
              <li>Status: <span style="color:orange">Active</span></li>
              <li v-if="c.fundsProgress > 50">Progress: <span class="green"> {{ c.fundsProgress }} % </span></li>
              <li v-else >Progress: <span class="red"> {{ c.fundsProgress }} % </span></li>              
            </ul>
            <ul v-else class="list-unstyled mt-3 mb-4">
              <li>Status: <span  style="color:green">Completed</span></li>
              <li>Comleted at:  {{ c.endDate }}</li>              
            </ul>
            <router-link v-if="c.isActive" class="btn btn-lg btn-block btn-outline-primary" :to="{ name: 'Details', params: { address: c.owner, isActive: c.isActive }}">Details</router-link>
            <router-link v-else style="color: green !important; border-color: green !important" class="btn btn-lg btn-block btn-outline-primary" :to="{ name: 'Details', params: { address: c.owner, isActive: c.isActive }}">Thank you! View history</router-link>
          </div>
        </div>
      </div>
     </div>
  </div>
</template>

<script>
      import ContractService from '../services/ContractResource';
      let contractService;

export default {
  name: 'Home',
  
  data () {
    return {  
      msg: 'Welcome to HaveFun Funds Dapp',
      address: "",
      campaigns :[],
      amount : 1,
      totalCollected: 0,
      totalDonations: 0
    }
  },
    methods: {
    init () { 
      this.contractService = new ContractService(this.$http)
      let self = this;
      this.contractService.getCampaignsInfo(function(info){
        self.campaigns = info.campaigns;
        self.totalCollected = info.totalFunds;
        self.totalDonations = info.allCampaings;
      })
    }
  },
  created () {
    this.init()
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

@media (min-width: 1200px)
{
  .container {
      max-width: 1270px !important;
  }
  .margin-fix{
    margin-right: 35px;
  }
}

.green{
  color: green;
}

.red{
  color: rgb(70, 41, 41);
}

</style>
