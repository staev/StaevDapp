const Donate = artifacts.require('./DonateContract.sol');

contract('DonateContract', function (accounts) {
	let tokenInstance; 
	
	const _owner = accounts[0];
	const _notOwner = accounts[1]; 
	

	console.log(_owner);
	
	 beforeEach(async function (){
	  tokenInstance = await Donate.new({ from: _owner});
	 });
		  
	 
	 
	 it("there should be not compaigns", async function(){
	  let compaignCounts = await tokenInstance.getAllCompaigns();
	  assert.strictEqual(compaignCounts.toNumber(), 0, "totalCompaings is not set to 0");  
	 })
	 
		  
	/* it("Should have no totalSupply", async function () { 
	  let totalSupply = await tokenInstance.totalSupply.call();
	  let castedTotalSupply = totalSupply.c[0];
	  assert.strictEqual(castedTotalSupply, _initialTotalSupply, " TotalSupply should be undefined" );
	 });
	 
	 it("should set name correctly", async function(){
	  let name = await tokenInstance.name.call();
	  
	  assert.strictEqual(name, _name,  "Should set name correctly");  
	 })
	 
	 
	 it("should set the symbol correctly", async function(){  
	  let symbol = await tokenInstance.symbol.call();
	  
	  assert.strictEqual(symbol, _symbol, "The expected symbol is not correct " + symbol );  
	 })
	 
	 it("should set decimals correctly", async function(){
	  let decimals = await tokenInstance.decimals.call();
	  let decimalNumber = decimals.c[0];
	  assert.strictEqual(decimalNumber, _decimals, "Should set decimals correctly");  
	 })

	it("can timetravel", async function () { 
		const dayInSeconds = 24 * 60 * 60;
		const timeBefore = await web3.eth.getBlock(web3.eth.blockNumber).timestamp;
		console.log(timeBefore);
		await timeTravel(web3, dayInSeconds);
		const timeAfter = await web3.eth.getBlock(web3.eth.blockNumber).timestamp;
		console.log(timeAfter);
		assert.isAtLeast(timeAfter - timeBefore, dayInSeconds, "It did not advance with a day " + (timeAfter - timeBefore)); 
	});*/
}); 