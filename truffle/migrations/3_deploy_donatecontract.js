var DonateContract = artifacts.require("./DonateContract.sol");
module.exports = function(deployer) {
  deployer.deploy(DonateContract);
};