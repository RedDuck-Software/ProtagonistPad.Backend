namespace Protagonist.Services;

public class ChainDataService
{
    public string? Url { get; } = "https://eth-rinkeby.alchemyapi.io/v2/pKK4LMJ3lNbADwuI2CkCPpal7dyj2kaE";
    public string? PrivateKey { get; } = "0x55abfd07209a3a3a16de91fe722e9cd523509156062af781172c76d47043dbe1";
    public long ChainId { get; }= 444444444500;
}

//var url = "https://eth-rinkeby.alchemyapi.io/v2/pKK4LMJ3lNbADwuI2CkCPpal7dyj2kaE";
//var privateKey = "0x55abfd07209a3a3a16de91fe722e9cd523509156062af781172c76d47043dbe1";
//"0x6080604052348015600f57600080fd5b5060ac8061001e6000396000f3fe6080604052348015600f57600080fd5b506004361060325760003560e01c806360fe47b11460375780636d4ce63c146049575b600080fd5b60476042366004605e565b600055565b005b60005460405190815260200160405180910390f35b600060208284031215606f57600080fd5b503591905056fea2646970667358221220d7d1e48ea321ce7f1e17568640c5365d4dcf3fcc95a5db14c54d5437caac56e964736f6c63430008090033";
// @"[{""inputs"":[],""name"":""get"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}]";
