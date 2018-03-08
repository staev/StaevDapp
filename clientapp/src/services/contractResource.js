export default class {  
  getContractAddress () {
    return '0xfb88de099e13c3ed21f80a7a1e49f8caecf10df6';
  }
  getContractAbi () {
    return [
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
      },
      {
        "inputs": [],
        "payable": false,
        "stateMutability": "nonpayable",
        "type": "constructor"
      },
      {
        "anonymous": false,
        "inputs": [
          {
            "indexed": false,
            "name": "oldValue",
            "type": "uint256"
          },
          {
            "indexed": false,
            "name": "newValue",
            "type": "uint256"
          }
        ],
        "name": "newValue",
        "type": "event"
      }
    ];
  }
}