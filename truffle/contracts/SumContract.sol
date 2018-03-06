pragma solidity ^0.4.18;
contract SumContract {

    uint private value;

    function SumContract() public {
        value =55;
    }
    
    function get() public view returns(uint){
        return value;
    }
    
    function increment() public {
       value += 1;
    }
    
}