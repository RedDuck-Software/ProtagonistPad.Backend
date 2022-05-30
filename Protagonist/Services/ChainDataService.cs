namespace Protagonist.Services;

public class ChainDataService
{
    public string? Url { get; } = "https://bsc.getblock.io/mainnet/?api_key=49e3ec5a-e609-4106-afa2-2b5e2bb30b30";
    public string? PrivateKey { get; } = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
    public long ChainId { get; }= 444444444500;
}
//"0x6080604052348015600f57600080fd5b5060ac8061001e6000396000f3fe6080604052348015600f57600080fd5b506004361060325760003560e01c806360fe47b11460375780636d4ce63c146049575b600080fd5b60476042366004605e565b600055565b005b60005460405190815260200160405180910390f35b600060208284031215606f57600080fd5b503591905056fea2646970667358221220d7d1e48ea321ce7f1e17568640c5365d4dcf3fcc95a5db14c54d5437caac56e964736f6c63430008090033";
// @"[{""inputs"":[],""name"":""get"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}]";
