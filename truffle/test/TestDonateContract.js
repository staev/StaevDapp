const Donate = artifacts.require('./DonateContract.sol');
contract('DonateContract', function (accounts) {
	let instance; 
	
	const _owner = accounts[0];
	const _campaignAcc = accounts[1]; 
	const _donator1 = accounts[2]; 
	const _donator2 = accounts[3]; 
	
	const oneEther = 10 ** 18;

	console.log(_owner);
	
	 beforeEach(async function (){
	  instance = await Donate.new({ from: _owner});
	 });
		  
	 
	 
	 it("there should be no campaign", async function(){
	  let campaignCounts = await instance.getAllCampaigns();
	  assert.strictEqual(campaignCounts.toNumber(), 0, "total Campaigns is not set to 0");  
	 })
	 
	 
	 it("should be 4 ethers collected after 2 times 2 ethers donated", async function () { 
		  
		  await instance.startCampaign(_campaignAcc, 10);		  
		  await instance.donateForCampaign(_campaignAcc,{from: _donator1, value : (2 * oneEther) });
		  await instance.donateForCampaign(_campaignAcc,{from: _donator2, value : (2 * oneEther) });

		 [totalNeeded, collectedUntilNow, giversCount]  = await  instance.campaignDetails(0);
		  
		 assert.strictEqual(collectedUntilNow.toNumber(), 4 * oneEther, " should be 4 ethers collected");
		 assert.strictEqual(giversCount.toNumber(), 2, " should be 2 donators");
	 });
	 
		  
	 it("should be no fund collected after campaign is started", async function () { 
		  
		  await instance.startCampaign(_campaignAcc, 10);

		  let allCampaingsCount = await instance.getAllCampaigns();
		  let [totalNeeded, collectedUntilNow, giversCount]  = await  instance.campaignDetails(0);
		  
		  assert.strictEqual(collectedUntilNow.toNumber(), 0 , " No donations at start" );
	 });
	 
	 it("should be no donators after campaign is started", async function () { 
		  
		  await instance.startCampaign(_campaignAcc, 10);
		  let [totalNeeded, collectedUntilNow, giversCount, isActive]  = await  instance.campaignDetails(0);
		  
		  assert.strictEqual(giversCount.toNumber(), 0 , " No givers at start" );
	 });
	 
	 it("campaign should be marked as active when created", async function () { 
		  
		  await instance.startCampaign(_campaignAcc, 10);
		  let [totalNeeded, collectedUntilNow, giversCount, isActive]  = await  instance.campaignDetails(0);
		  
		  assert.strictEqual(isActive, true , " Should be active" );
	 });
	 
	 
	 
	 it("contract balance should be 0 after campaign funds are collected", async function () {
		  await instance.startCampaign(_campaignAcc, 7);
		  
		  await instance.donateForCampaign(_campaignAcc,{from: _donator1, value : (3 * oneEther) });
		  await instance.donateForCampaign(_campaignAcc,{from: _donator2, value : (2 * oneEther) });
		  
		  let balance = await  instance.getBalance({from: _owner});
		  assert.strictEqual(balance.toNumber(), 5 * oneEther , " balance should be 5 ethers" );
		  
		  await instance.donateForCampaign(_campaignAcc,{from: _donator2, value : (3 * oneEther) });
		  
		  let newBalance = await  instance.getBalance({from: _owner});
		  assert.strictEqual(newBalance.toNumber(), 0 , " balance should be 0 ethers" );
	 });
	 
	 it("campaign should be not active after funds are collected", async function () {
		await instance.startCampaign(_campaignAcc, 2);
	  
		await instance.donateForCampaign(_campaignAcc,{from: _donator1, value : (3 * oneEther) });
		let [totalNeeded, collectedUntilNow, giversCount, isActive]  = await  instance.campaignDetails(0);
		  
		assert.strictEqual(isActive, false , " No givers at start" );
	 });
}); 