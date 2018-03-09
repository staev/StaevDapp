<template>
  <div id="app">
        <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom box-shadow">
      <h5 class="my-0 mr-md-auto font-weight-normal">HaveFun Funds</h5>
      <span class="p-2 text-dark" href="#">Address:</span>
      <span class="p-2 text-dark" href="#">0x6330a553fc93768f612722bb8c2ec78ac90b3bbc</span>
       
      <a class="btn btn-outline-primary" v-on:click="donateForOwner" href="#">Donate</a>
      <!--<a class="btn btn-outline-primary" v-on:click="donateForOwner" href="#">New Campaign</a>-->
    </div>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App',
   data () {
    return {  
      address: "",
      isContractOwner : false
    }
  },
    methods: {
    init () { 
     
    }
  },
  created () {
    this.init()
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
