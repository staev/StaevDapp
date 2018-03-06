var SumContract = artifacts.require("./SumContract.sol");
module.exports = function(deployer) {
  deployer.deploy(SumContract);
};