<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <button v-on:click="click">mada facka</button>
  </div>
</template>

<script>

      import Web3 from 'web3'
      let provider = new Web3(new Web3.providers.HttpProvider("http://localhost:8545"));

export default {
  name: 'HelloWorld',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  },
    methods: {
    init () { 
        
    },
    click(){
      debugger

        provider.eth.getAccounts((error, accounts) => console.log(accounts))
        provider.eth.getBalance("0x02ab6efddb2682d888f4cfa6e3f94a5d4085e444", 
                (error, balance) => console.log(balance.toNumber()))
        web3.eth.getAccounts((error, accounts) => console.log(accounts))
       
        //let acc = web3.eth.accounts[0]; //get the first account
       // let otherAcc = web3.eth.accounts[1]; 
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
