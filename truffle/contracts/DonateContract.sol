pragma solidity ^0.4.18;

contract DonateContract {
    
    struct DonationInfo {
        address giver;
        uint amount;
        uint date;
    }
    
     struct CompaignInfo {
        address holder; 
        uint creationDate;
        uint collectedFunds;
        uint totalFundsNeeded;
        bool isActive;
        uint fundsCollectedAt;
    }
    
    address private owner;
    uint private totalCompaings;
    uint private totalFundsDonated;
    
    mapping(address => CompaignInfo) private compaigns;
    mapping(address => DonationInfo[]) private donationDetails;
    
    event CompaingStarted(address owner, uint fundsNeeded, uint date);
    event FundsDonated(address from, address compaign, uint fundsAmount, uint date);
    event ContractDonated(address from, uint fundsAmount, uint date);
     
    function DonateContract() public {
        owner = msg.sender;
        totalCompaings = 0;
        totalFundsDonated = 0;
    }
    
    modifier isOwner() {
        require(msg.sender == owner);
        _;
    }
    
    modifier isOverflow(address _compaignAddr) {
        uint compaignFunds = compaigns[_compaignAddr].collectedFunds;
        require(compaignFunds + msg.value > compaignFunds);
         _;
    }
    
    modifier isValidCompaign(address _addr) {
       require(compaigns[_addr].holder == _addr);
       require(compaigns[_addr].isActive == true);
       _;
    }
    
    modifier isNewCompaign(address _addr) {
        require(compaigns[_addr].holder != _addr);
       _;
    }
    
    function getAllCompaigns() public view returns(uint) {
        return totalCompaings;
    }
    
    function donate() payable public {
        ContractDonated(msg.sender, msg.value, now);
    }
    
    function startCompaign(address _addr, uint fundsNeeded) isOwner isNewCompaign(_addr) public {
       CompaignInfo memory cInfo;
       cInfo.creationDate = now;
       cInfo.holder = _addr;
       cInfo.isActive = true;
       cInfo.totalFundsNeeded = fundsNeeded * 1 ether;
       cInfo.collectedFunds = 0;
   
       compaigns[_addr] = cInfo;
       
       totalCompaings +=1;
       
       CompaingStarted(_addr, fundsNeeded, now);
    }
    
    function isDonateMoreThanRemaining(address _addr) private view returns(bool) {
        bool isMore = compaigns[_addr].collectedFunds + msg.value > compaigns[_addr].totalFundsNeeded;
        return isMore;
    }
    
    function donateForCompaign(address compaignAddr) 
            public isValidCompaign(compaignAddr) isOverflow(compaignAddr) payable {
    
        uint sumToTranfer = msg.value;
        
        // if sum us more take only needed
        if(isDonateMoreThanRemaining(compaignAddr)) {
            sumToTranfer =  compaigns[compaignAddr].totalFundsNeeded - compaigns[compaignAddr].collectedFunds;
        }
        
        compaigns[compaignAddr].collectedFunds += sumToTranfer;
        compaignAddr.transfer(sumToTranfer);
        DonationInfo memory info = DonationInfo({
                giver : msg.sender,
                amount : sumToTranfer,
                date : now
            });
            
        donationDetails[compaignAddr].push(info);
        totalFundsDonated += sumToTranfer;
        
        FundsDonated(msg.sender, compaignAddr, sumToTranfer , info.date);
        
        // money are collected 
        if(compaigns[compaignAddr].collectedFunds >= compaigns[compaignAddr].totalFundsNeeded) {
            compaigns[compaignAddr].isActive = false;
            compaigns[compaignAddr].fundsCollectedAt = info.date;
        }
    }
    
    function allCompaignsInfo() public view returns(uint totalFunds, uint allCompaings) {
        return (totalFunds = totalFundsDonated, allCompaings = totalCompaings);
    }
    
    function compaignDetails(address _addr) view public returns(uint totalNeeded, uint collectedUntilNow, uint giversCount){
        return (
                totalNeeded = compaigns[_addr].totalFundsNeeded,
                collectedUntilNow = compaigns[_addr].collectedFunds,
                giversCount = donationDetails[_addr].length
            );
    }
    
    function giverDetails(address compaignAddr, uint index) public view 
        returns (address giver, uint amount, uint date) {
        
        DonationInfo memory giverInfo = donationDetails[compaignAddr][index];
        return (giver = giverInfo.giver, amount = giverInfo.amount, date = giverInfo.date);
    }
    
    function getTotalBalance() public isOwner view returns(uint) {
        return this.balance;
    }
}