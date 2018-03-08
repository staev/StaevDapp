pragma solidity ^0.4.18;
contract SumContract {

    uint private value;
    event newValue(uint oldValue, uint newValue);
    
    function SumContract() public {
        value =55;
    }
    
    function get() public view returns(uint){
        return value;
    }
    
    function increment() public {
        uint oldValue = value;
       value += 1;
       newValue(oldValue,value);
    }   
}