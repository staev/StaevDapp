pragma solidity ^0.4.18;

contract DonateContract {
    
    struct DonationInfo {
        address giver;
        uint amount;
        uint date;
    }
    
     struct CampaignInfo {
        address holder; 
        uint creationDate;
        uint collectedFunds;
        uint totalFundsNeeded;
        bool isActive;
        uint fundsCollectedAt;
    }
    
    address private owner;
    uint private totalCampaings;
    uint private totalFundsDonated;
    
    mapping(address => CampaignInfo) private campaigns;
    mapping(address => DonationInfo[]) private donationDetails;
    
    //helper mappings for iterating 
    mapping(uint => address) campaignIndexes;
    
    
    event CampaingStarted(address owner, uint fundsNeeded, uint date);
    event FundsDonated(address from, address campaign, uint fundsAmount, uint date);
    event ContractDonated(address from, uint fundsAmount, uint date);
     
    function DonateContract() public {
        owner = msg.sender;
        totalCampaings = 0;
        totalFundsDonated = 0;
    }
    
    modifier isOwner() {
        require(msg.sender == owner);
        _;
    }
    
    modifier isMoreThanZero() {
       require(msg.value > 0);
         _;
    }
    
    modifier isOverflow(address _campaignAddr) {
        uint campaignFunds = campaigns[_campaignAddr].collectedFunds;
        require(campaignFunds + msg.value > campaignFunds);
         _;
    }
    
    modifier isValidCampaign(address _addr) {
       require(campaigns[_addr].holder == _addr);
       require(campaigns[_addr].isActive == true);
       _;
    }
    
    modifier isNewCampaign(address _addr) {
        require(campaigns[_addr].holder != _addr);
       _;
    }
    
    function getAllCampaigns() public view returns(uint) {
        return totalCampaings;
    }
    
    function donate() payable public {
        ContractDonated(msg.sender, msg.value, now);
    }
    
    function startCampaign(address _addr, uint fundsNeeded) isOwner isNewCampaign(_addr) public {
       CampaignInfo memory cInfo;
       cInfo.creationDate = now;
       cInfo.holder = _addr;
       cInfo.isActive = true;
       cInfo.totalFundsNeeded = fundsNeeded * 1 ether;
       cInfo.collectedFunds = 0;
       cInfo.fundsCollectedAt = 0;
   
       campaigns[_addr] = cInfo;
       
       campaignIndexes[totalCampaings] = _addr;
       totalCampaings +=1;
       
       CampaingStarted(_addr, cInfo.totalFundsNeeded , now);
    }
    
    function isDonateMoreThanRemaining(address _addr) private view returns(bool) {
        bool isMore = campaigns[_addr].collectedFunds + msg.value > campaigns[_addr].totalFundsNeeded;
        return isMore;
    }
    
    function donateForCampaign(address campaignAddr) 
            public isValidCampaign(campaignAddr) isMoreThanZero isOverflow(campaignAddr) payable {
            
       
        uint sumToTranfer = msg.value;
         
        // if sum us more take only needed
        if(isDonateMoreThanRemaining(campaignAddr)) {
            sumToTranfer =  campaigns[campaignAddr].totalFundsNeeded - campaigns[campaignAddr].collectedFunds;
            uint moneyBack = msg.value - sumToTranfer;
            msg.sender.transfer(moneyBack);
        }
        
        campaigns[campaignAddr].collectedFunds += sumToTranfer;
       
        DonationInfo memory info = DonationInfo({
                giver : msg.sender,
                amount : sumToTranfer,
                date : now
            });
            
        donationDetails[campaignAddr].push(info);
        totalFundsDonated += sumToTranfer;
        
        FundsDonated(msg.sender, campaignAddr, sumToTranfer , info.date);
       
        // money are collected 
        if(campaigns[campaignAddr].collectedFunds >= campaigns[campaignAddr].totalFundsNeeded) {
            campaigns[campaignAddr].isActive = false;
            campaigns[campaignAddr].fundsCollectedAt = info.date;
            campaignAddr.transfer(campaigns[campaignAddr].collectedFunds);
        }
    }
    
    function allCampaignsInfo() public view returns(uint totalFunds, uint allCampaings) {
        return (totalFunds = totalFundsDonated, allCampaings = totalCampaings);
    }
    
    function campaignDetails(uint index) view public 
            returns(address campaignOwner, uint totalNeeded, uint collectedUntilNow, uint giversCount, bool isActive, uint created, uint donatedEnd){
        
        address _addr = campaignIndexes[index];
        
        return (
                campaignOwner = _addr,
                totalNeeded = campaigns[_addr].totalFundsNeeded,
                collectedUntilNow = campaigns[_addr].collectedFunds,
                giversCount = donationDetails[_addr].length,
                isActive = campaigns[_addr].isActive,
                created = campaigns[_addr].creationDate,
                donatedEnd = campaigns[_addr].fundsCollectedAt
            );
    }
    
    function giverDetails(address campaignAddr, uint index) public view 
        returns (address giver, uint amount, uint date) {
        
        DonationInfo memory giverInfo = donationDetails[campaignAddr][index];
        return (giver = giverInfo.giver, amount = giverInfo.amount, date = giverInfo.date);
    }
    
    function hasOwnerRights() public view returns(bool){
        return msg.sender == owner;
    }
    
   function getBalance() public isOwner view returns(uint){
        return this.balance;
    }
}