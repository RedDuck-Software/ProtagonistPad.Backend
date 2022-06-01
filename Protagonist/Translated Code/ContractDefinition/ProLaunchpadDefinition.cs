using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;


// ReSharper disable InconsistentNaming
namespace Example.Contracts.ProLaunchpad.ContractDefinition
{


    public partial class ProLaunchpadDeployment : ProLaunchpadDeploymentBase
    {
        public ProLaunchpadDeployment() : base(BYTECODE) { }
        public ProLaunchpadDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProLaunchpadDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6101006040523480156200001257600080fd5b50604051620022db380380620022db8339810160408190526200003591620002b4565b620000403362000247565b600180556001600160a01b038616620000a05760405162461bcd60e51b815260206004820152601f60248201527f504c3a207a65726f2061646472657373206973206e6f7420616c6c6f7765640060448201526064015b60405180910390fd5b6001600160a01b038516620000f85760405162461bcd60e51b815260206004820152601f60248201527f504c3a207a65726f2061646472657373206973206e6f7420616c6c6f77656400604482015260640162000097565b82841015620001245760405162461bcd60e51b8152602060048201526000602482015260440162000097565b4282116200014f5760405162461bcd60e51b8152602060048201526000602482015260440162000097565b8082106200017a5760405162461bcd60e51b8152602060048201526000602482015260440162000097565b6200018942626ebe0062000311565b8110620001ff5760405162461bcd60e51b815260206004820152603860248201527f504c3a20656e6454696d65206e6565647320746f206265206c6573732074686160448201527f6e203132207765656b7320696e20746865206675747572650000000000000000606482015260840162000097565b600280546001600160a01b039788166001600160a01b031991821617909155600380549690971695169490941790945560809190915260a05260c09190915260e05262000338565b600080546001600160a01b038381166001600160a01b0319831681178455604051919092169283917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09190a35050565b80516001600160a01b0381168114620002af57600080fd5b919050565b60008060008060008060c08789031215620002ce57600080fd5b620002d98762000297565b9550620002e96020880162000297565b945060408701519350606087015192506080870151915060a087015190509295509295509295565b600082198211156200033357634e487b7160e01b600052601160045260246000fd5b500190565b60805160a05160c05160e051611f23620003b86000396000818161040b0152818161052c0152818161078601528181610ef501528181610f3401528181611359015261139801526000818161022501526107160152600081816103230152610cbb0152600081816104580152818161082b0152610c360152611f236000f3fe608060405234801561001057600080fd5b50600436106101cf5760003560e01c806384dae94211610104578063c234747b116100a2578063ee9ddd4711610071578063ee9ddd471461042d578063f2fde38b14610440578063fb86a40414610453578063fc0c546a1461047a57600080fd5b8063c234747b14610360578063cc49ede714610369578063e632c2f3146103fd578063ed338ff11461040657600080fd5b8063906a26e0116100de578063906a26e01461031e578063a035b1fe14610345578063a89989dd1461034e578063be9a65551461035757600080fd5b806384dae942146102f7578063872a7810146103005780638da5cb5b1461030d57600080fd5b8063484f4ea911610171578063715018a61161014b578063715018a6146102c057806373913545146102c857806374a8f103146102db5780637abc090f146102ee57600080fd5b8063484f4ea91461026f57806353c426351461029a57806368742da6146102ad57600080fd5b806313d033c0116101ad57806313d033c0146102175780631cbaee2d1461022057806322ea2231146102475780632e0876a01461025c57600080fd5b806307b1a929146101d45780630e246273146101fc5780630fb5a6b41461020e575b600080fd5b6101e76101e2366004611bed565b61048d565b60405190151581526020015b60405180910390f35b600e545b6040519081526020016101f3565b610200600a5481565b61020060095481565b6102007f000000000000000000000000000000000000000000000000000000000000000081565b61025a610255366004611c1d565b6106ec565b005b61025a61026a366004611c1d565b6109cf565b600254610282906001600160a01b031681565b6040516001600160a01b0390911681526020016101f3565b61025a6102a8366004611c55565b610c34565b6101e76102bb366004611ca2565b610ef0565b61025a611051565b61025a6102d6366004611bed565b611087565b61025a6102e9366004611ca2565b611186565b610200600b5481565b61020060065481565b600c546101e79060ff1681565b6000546001600160a01b0316610282565b6102007f000000000000000000000000000000000000000000000000000000000000000081565b61020060075481565b61020060045481565b61020060085481565b610200600f5481565b6103d5610377366004611ca2565b6001600160a01b03166000908152600d602090815260409182902082516080810184528154808252600183015493820184905260029092015460ff808216151595830186905261010090910416151560609091018190529093919291565b60408051948552602085019390935290151591830191909152151560608201526080016101f3565b61020060055481565b6102007f000000000000000000000000000000000000000000000000000000000000000081565b6101e761043b366004611ca2565b611354565b61025a61044e366004611ca2565b611462565b6102007f000000000000000000000000000000000000000000000000000000000000000081565b600354610282906001600160a01b031681565b600080546001600160a01b031633146104c15760405162461bcd60e51b81526004016104b890611cbd565b60405180910390fd5b60006005541161052a5760405162461bcd60e51b815260206004820152602e60248201527f504c3a20746865206c61756e636870616420686164206e6f74206265656e206960448201526d1b9a5d1a585b1a5e9959081e595d60921b60648201526084016104b8565b7f0000000000000000000000000000000000000000000000000000000000000000156105ab5760405162461bcd60e51b815260206004820152602a60248201527f504c3a20746865206c61756e63687061642063616e2062652073746172746564604482015269206f6e6c79206f6e636560b01b60648201526084016104b8565b600082116105fb5760405162461bcd60e51b815260206004820152601c60248201527f504c3a2070726963652073686f756c64206e6f74206265207a65726f0000000060448201526064016104b8565b60078290556003546040516370a0823160e01b81523060048201526001600160a01b03909116906370a0823190602401602060405180830381865afa158015610648573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061066c9190611cf2565b60048190556106e35760405162461bcd60e51b815260206004820152603d60248201527f504c3a20746865206c61756e63687061642073686f756c6420626520696e697460448201527f69616c697a6564207769746820636c61696d61626c6520746f6b656e7300000060648201526084016104b8565b5060015b919050565b6002600154141561070f5760405162461bcd60e51b81526004016104b890611d0b565b60026001557f00000000000000000000000000000000000000000000000000000000000000004210156107845760405162461bcd60e51b815260206004820152601f60248201527f504c3a207468652073616c65206973206e6f742073746172746564207965740060448201526064016104b8565b7f000000000000000000000000000000000000000000000000000000000000000042106107ec5760405162461bcd60e51b815260206004820152601660248201527514130e881d1a19481cd85b19481a5cc818db1bdcd95960521b60448201526064016104b8565b6000670de0b6b3a7640000600754836108059190611d58565b61080f9190611d77565b90506108283360025483906001600160a01b03166114fd565b507f0000000000000000000000000000000000000000000000000000000000000000816006546108589190611d99565b11156108b05760405162461bcd60e51b815260206004820152602160248201527f504c3a20707572636861736520776f756c6420657863656564206d61702063616044820152600760fc1b60648201526084016104b8565b6108c8336002546001600160a01b03169030846115e0565b6108d28383611651565b81600560008282546108e49190611d99565b9250508190555080600660008282546108fd9190611d99565b9091555050600454600554111561097c5760405162461bcd60e51b815260206004820152603a60248201527f504c3a2070757263686173696e6720617474656d70742065786365656473207460448201527f6f74616c436c61696d61626c65416d6f756e7420616d6f756e7400000000000060648201526084016104b8565b604080516001600160a01b0385168152602081018490529081018290527f22bb9e718001d9e96ba01b874e20a59101db4428f08f3b5d72f457b03a5579ab9060600160405180910390a150506001805550565b600260015414156109f25760405162461bcd60e51b81526004016104b890611d0b565b600260018181556001600160a01b0384166000908152600d6020526040902090910154839161010090910460ff16151514610a2c57600080fd5b6001600160a01b0381166000908152600d602052604090206002015460ff1615610a5557600080fd5b6001600160a01b038381166000818152600d602052604081209054909233928314929116148180610a835750805b610af55760405162461bcd60e51b815260206004820152603860248201527f504c3a206f6e6c792062656e656669636961727920616e64206f776e6572206360448201527f616e2072656c656173652076657374656420746f6b656e73000000000000000060648201526084016104b8565b604080516080810182528454815260018501546020820152600285015460ff808216151593830193909352610100900490911615156060820152600090610b3b90611813565b905085811015610ba95760405162461bcd60e51b815260206004820152603360248201527f504c3a2063616e6e6f742072656c6561736520746f6b656e732c206e6f7420656044820152726e6f7567682076657374656420746f6b656e7360681b60648201526084016104b8565b6001840154610bb890876118f5565b6001850155600f54610bca9087611901565b600f55600354610be4906001600160a01b0316888861190d565b866001600160a01b03167fe9aa550fd75d0d28e07fa9dd67d3ae705678776f6c4a75abd09534f93e7d790787604051610c1f91815260200190565b60405180910390a25050600180555050505050565b7f00000000000000000000000000000000000000000000000000000000000000006006541115610cb95760405162461bcd60e51b815260206004820152602a60248201527f504c3a20746f74616c4255534452656365697665642073686f756c642062652060448201526903c3d20686172644361760b41b60648201526084016104b8565b7f00000000000000000000000000000000000000000000000000000000000000006006541015610d3e5760405162461bcd60e51b815260206004820152602a60248201527f504c3a20746f74616c4255534452656365697665642073686f756c642062652060448201526903e3d20736f66744361760b41b60648201526084016104b8565b6000546001600160a01b03163314610d685760405162461bcd60e51b81526004016104b890611cbd565b42851015610dcc5760405162461bcd60e51b815260206004820152602b60248201527f504c3a2073746172742076657374696e67206e6565647320746f20626520696e60448201526a207468652066757475726560a81b60648201526084016104b8565b82841115610e135760405162461bcd60e51b815260206004820152601460248201527328261d1031b634b333101f10323ab930ba34b7b760611b60448201526064016104b8565b60008311610e635760405162461bcd60e51b815260206004820152601860248201527f504c3a206475726174696f6e206d757374206265203e2030000000000000000060448201526064016104b8565b6001821015610ec05760405162461bcd60e51b815260206004820152602360248201527f54563a20736c696365506572696f645365636f6e6473206d757374206265203e6044820152623d203160e81b60648201526084016104b8565b6008859055610ecf85856118f5565b600955600a92909255600b55600c805460ff19169115159190911790555050565b6000807f000000000000000000000000000000000000000000000000000000000000000011610f315760405162461bcd60e51b81526004016104b890611db1565b427f000000000000000000000000000000000000000000000000000000000000000010610f705760405162461bcd60e51b81526004016104b890611dfb565b6000546001600160a01b03163314610f9a5760405162461bcd60e51b81526004016104b890611cbd565b60026001541415610fbd5760405162461bcd60e51b81526004016104b890611d0b565b60026001819055546040516370a0823160e01b81523060048201526110479184916001600160a01b03909116906370a0823190602401602060405180830381865afa158015611010573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906110349190611cf2565b6002546001600160a01b0316919061190d565b5050600180805590565b6000546001600160a01b0316331461107b5760405162461bcd60e51b81526004016104b890611cbd565b6110856000611942565b565b6000546001600160a01b031633146110b15760405162461bcd60e51b81526004016104b890611cbd565b6010546003546110cf916001600160a01b03908116918491166114fd565b506003546010546040516323b872dd60e01b81526001600160a01b039182166004820152306024820152604481018490529116906323b872dd906064016020604051808303816000875af115801561112b573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061114f9190611e3e565b506040518181527f3677c9434f5ab443cebe570bd0549adb7149a47c57a250985034f42dbea50e469060200160405180910390a150565b6000546001600160a01b031633146111b05760405162461bcd60e51b81526004016104b890611cbd565b6001600160a01b0381166000908152600d6020526040902060020154819060ff6101009091041615156001146111e557600080fd5b6001600160a01b0381166000908152600d602052604090206002015460ff161561120e57600080fd5b6001600160a01b0382166000908152600d60205260409020600c5460ff16151560011461127d5760405162461bcd60e51b815260206004820152601c60248201527f54563a2076657374696e67206973206e6f74207265766f6361626c650000000060448201526064016104b8565b604080516080810182528254815260018301546020820152600283015460ff8082161515938301939093526101009004909116151560608201526000906112c390611813565b905080156112d5576112d584826109cf565b600182015482546000916112e99190611901565b600f549091506112f99082611901565b600f5560028301805460ff19166001179055604080516001600160a01b0387168152602081018490527fceb5abccbe6923f3621bf6248d1ad2e173cd8d55b1174fcd03d299b375fe9c54910160405180910390a15050505050565b6000807f0000000000000000000000000000000000000000000000000000000000000000116113955760405162461bcd60e51b81526004016104b890611db1565b427f0000000000000000000000000000000000000000000000000000000000000000106113d45760405162461bcd60e51b81526004016104b890611dfb565b6000546001600160a01b031633146113fe5760405162461bcd60e51b81526004016104b890611cbd565b600260015414156114215760405162461bcd60e51b81526004016104b890611d0b565b600260015560055460045460009161143891611e5b565b600354909150611452906001600160a01b0316848361190d565b5050600060045550600180805590565b6000546001600160a01b0316331461148c5760405162461bcd60e51b81526004016104b890611cbd565b6001600160a01b0381166114f15760405162461bcd60e51b815260206004820152602660248201527f4f776e61626c653a206e6577206f776e657220697320746865207a65726f206160448201526564647265737360d01b60648201526084016104b8565b6114fa81611942565b50565b604051636eb1769f60e11b81526001600160a01b038481166004830152306024830152600091829184169063dd62ed3e90604401602060405180830381865afa15801561154e573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906115729190611cf2565b9050808411156115d35760405162461bcd60e51b815260206004820152602660248201527f504c3a206d616b65207375726520746f2061646420656e6f7567687420616c6c6044820152656f77616e636560d01b60648201526084016104b8565b60019150505b9392505050565b6040516001600160a01b038085166024830152831660448201526064810182905261164b9085906323b872dd60e01b906084015b60408051601f198184030181529190526020810180516001600160e01b03166001600160e01b031990931692909217909152611992565b50505050565b6000811161169a5760405162461bcd60e51b81526020600482015260166024820152750504c3a20616d6f756e74206d757374206265203e20360541b60448201526064016104b8565b6001600160a01b0382166000908152600d60209081526040918290208251608081018452815480825260018301549382019390935260029091015460ff8082161515948301949094526101009004909216151560608301526117b1575060408051608081018252828152600060208083018281528385018381526001606086018181526001600160a01b038a16808752600d90955296852086518155925183820155905160029092018054965115156101000261ff00199315159390931661ffff199097169690961791909117909455600e805494850181559091527fbb7b4a454dc3493923482f07822329ed19e8244eff582cc204f8554c3620c3fd90920180546001600160a01b0319169092179091556117c0565b80516117bd90836118f5565b81525b600f546117cd90836118f5565b600f556040518281526001600160a01b038416907f2f5488c5b0350655053f3d00e1693b26a49ecd0242b2c0df881ccda054490d309060200160405180910390a2505050565b600954600090429081108061182e5750604083015115156001145b1561183c5750600092915050565b600a5460085461184b916118f5565b811061186157602083015183516115d991611901565b60006118786008548361190190919063ffffffff16565b90506000611891600b5483611a6490919063ffffffff16565b905060006118aa600b5483611a7090919063ffffffff16565b905060006118d1600a546118cb848a60000151611a7090919063ffffffff16565b90611a64565b90506118ea87602001518261190190919063ffffffff16565b979650505050505050565b60006115d98284611d99565b60006115d98284611e5b565b6040516001600160a01b03831660248201526044810182905261193d90849063a9059cbb60e01b90606401611614565b505050565b600080546001600160a01b038381166001600160a01b0319831681178455604051919092169283917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09190a35050565b60006119e7826040518060400160405280602081526020017f5361666545524332303a206c6f772d6c6576656c2063616c6c206661696c6564815250856001600160a01b0316611a7c9092919063ffffffff16565b80519091501561193d5780806020019051810190611a059190611e3e565b61193d5760405162461bcd60e51b815260206004820152602a60248201527f5361666545524332303a204552433230206f7065726174696f6e20646964206e6044820152691bdd081cdd58d8d9595960b21b60648201526084016104b8565b60006115d98284611d77565b60006115d98284611d58565b6060611a8b8484600085611a93565b949350505050565b606082471015611af45760405162461bcd60e51b815260206004820152602660248201527f416464726573733a20696e73756666696369656e742062616c616e636520666f6044820152651c8818d85b1b60d21b60648201526084016104b8565b6001600160a01b0385163b611b4b5760405162461bcd60e51b815260206004820152601d60248201527f416464726573733a2063616c6c20746f206e6f6e2d636f6e747261637400000060448201526064016104b8565b600080866001600160a01b03168587604051611b679190611e9e565b60006040518083038185875af1925050503d8060008114611ba4576040519150601f19603f3d011682016040523d82523d6000602084013e611ba9565b606091505b50915091506118ea82828660608315611bc35750816115d9565b825115611bd35782518084602001fd5b8160405162461bcd60e51b81526004016104b89190611eba565b600060208284031215611bff57600080fd5b5035919050565b80356001600160a01b03811681146106e757600080fd5b60008060408385031215611c3057600080fd5b611c3983611c06565b946020939093013593505050565b80151581146114fa57600080fd5b600080600080600060a08688031215611c6d57600080fd5b853594506020860135935060408601359250606086013591506080860135611c9481611c47565b809150509295509295909350565b600060208284031215611cb457600080fd5b6115d982611c06565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b600060208284031215611d0457600080fd5b5051919050565b6020808252601f908201527f5265656e7472616e637947756172643a207265656e7472616e742063616c6c00604082015260600190565b634e487b7160e01b600052601160045260246000fd5b6000816000190483118215151615611d7257611d72611d42565b500290565b600082611d9457634e487b7160e01b600052601260045260246000fd5b500490565b60008219821115611dac57611dac611d42565b500190565b6020808252602a908201527f504c3a20746865206c61756e636870616420686173206e6f74206265656e20736040820152691d185c9d1959081e595d60b21b606082015260800190565b60208082526023908201527f504c3a20746865206c61756e636870616420686173206e6f7420656e646564206040820152621e595d60ea1b606082015260800190565b600060208284031215611e5057600080fd5b81516115d981611c47565b600082821015611e6d57611e6d611d42565b500390565b60005b83811015611e8d578181015183820152602001611e75565b8381111561164b5750506000910152565b60008251611eb0818460208701611e72565b9190910192915050565b6020815260008251806020840152611ed9816040850160208701611e72565b601f01601f1916919091016040019291505056fea2646970667358221220fec02dd86636f749eb2e437a41c51440c258882a8895696f8fc4dcb6acf8c76764736f6c634300080a0033";
        public ProLaunchpadDeploymentBase() : base(BYTECODE) { }
        public ProLaunchpadDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_BUSD", 1)]
        public virtual string BUSD { get; set; }
        [Parameter("address", "_launchedToken", 2)]
        public virtual string LaunchedToken { get; set; }
        [Parameter("uint256", "_hardCap", 3)]
        public virtual BigInteger HardCap { get; set; }
        [Parameter("uint256", "_softCap", 4)]
        public virtual BigInteger SoftCap { get; set; }
        [Parameter("uint256", "_saleStartTime", 5)]
        public virtual BigInteger SaleStartTime { get; set; }
        [Parameter("uint256", "_saleEndTime", 6)]
        public virtual BigInteger SaleEndTime { get; set; }
    }

    public partial class BUSDFunction : BUSDFunctionBase { }

    [Function("BUSD", "address")]
    public class BUSDFunctionBase : FunctionMessage
    {

    }

    public partial class ClaimVestedTokensFunction : ClaimVestedTokensFunctionBase { }

    [Function("claimVestedTokens")]
    public class ClaimVestedTokensFunctionBase : FunctionMessage
    {
        [Parameter("address", "_beneficiary", 1)]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class CliffFunction : CliffFunctionBase { }

    [Function("cliff", "uint256")]
    public class CliffFunctionBase : FunctionMessage
    {

    }

    public partial class DurationFunction : DurationFunctionBase { }

    [Function("duration", "uint256")]
    public class DurationFunctionBase : FunctionMessage
    {

    }

    public partial class FundVestingFunction : FundVestingFunctionBase { }

    [Function("fundVesting")]
    public class FundVestingFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_totalTokens", 1)]
        public virtual BigInteger TotalTokens { get; set; }
    }

    public partial class GetVestingFunction : GetVestingFunctionBase { }

    [Function("getVesting", typeof(GetVestingOutputDTO))]
    public class GetVestingFunctionBase : FunctionMessage
    {
        [Parameter("address", "_beneficiary", 1)]
        public virtual string Beneficiary { get; set; }
    }

    public partial class GetVestingCountFunction : GetVestingCountFunctionBase { }

    [Function("getVestingCount", "uint256")]
    public class GetVestingCountFunctionBase : FunctionMessage
    {

    }

    public partial class HardCapFunction : HardCapFunctionBase { }

    [Function("hardCap", "uint256")]
    public class HardCapFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class PriceFunction : PriceFunctionBase { }

    [Function("price", "uint256")]
    public class PriceFunctionBase : FunctionMessage
    {

    }

    public partial class PurchaseTokensFunction : PurchaseTokensFunctionBase { }

    [Function("purchaseTokens")]
    public class PurchaseTokensFunctionBase : FunctionMessage
    {
        [Parameter("address", "_beneficiary", 1)]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class RevocableFunction : RevocableFunctionBase { }

    [Function("revocable", "bool")]
    public class RevocableFunctionBase : FunctionMessage
    {

    }

    public partial class RevokeFunction : RevokeFunctionBase { }

    [Function("revoke")]
    public class RevokeFunctionBase : FunctionMessage
    {
        [Parameter("address", "_beneficiary", 1)]
        public virtual string Beneficiary { get; set; }
    }

    public partial class SaleEndTimeFunction : SaleEndTimeFunctionBase { }

    [Function("saleEndTime", "uint256")]
    public class SaleEndTimeFunctionBase : FunctionMessage
    {

    }

    public partial class SaleStartTimeFunction : SaleStartTimeFunctionBase { }

    [Function("saleStartTime", "uint256")]
    public class SaleStartTimeFunctionBase : FunctionMessage
    {

    }

    public partial class SetVestingFunction : SetVestingFunctionBase { }

    [Function("setVesting")]
    public class SetVestingFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_start", 1)]
        public virtual BigInteger Start { get; set; }
        [Parameter("uint256", "_cliff", 2)]
        public virtual BigInteger Cliff { get; set; }
        [Parameter("uint256", "_duration", 3)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint256", "_slicePeriodSeconds", 4)]
        public virtual BigInteger SlicePeriodSeconds { get; set; }
        [Parameter("bool", "_revocable", 5)]
        public virtual bool Revocable { get; set; }
    }

    public partial class SlicePeriodSecondsFunction : SlicePeriodSecondsFunctionBase { }

    [Function("slicePeriodSeconds", "uint256")]
    public class SlicePeriodSecondsFunctionBase : FunctionMessage
    {

    }

    public partial class SoftCapFunction : SoftCapFunctionBase { }

    [Function("softCap", "uint256")]
    public class SoftCapFunctionBase : FunctionMessage
    {

    }

    public partial class StartFunction : StartFunctionBase { }

    [Function("start", "uint256")]
    public class StartFunctionBase : FunctionMessage
    {

    }

    public partial class StartPurchaseFunction : StartPurchaseFunctionBase { }

    [Function("startPurchase", "bool")]
    public class StartPurchaseFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_price", 1)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class TokenFunction : TokenFunctionBase { }

    [Function("token", "address")]
    public class TokenFunctionBase : FunctionMessage
    {

    }

    public partial class TotalBUSDReceivedFunction : TotalBUSDReceivedFunctionBase { }

    [Function("totalBUSDReceived", "uint256")]
    public class TotalBUSDReceivedFunctionBase : FunctionMessage
    {

    }

    public partial class TotalClaimableAmountFunction : TotalClaimableAmountFunctionBase { }

    [Function("totalClaimableAmount", "uint256")]
    public class TotalClaimableAmountFunctionBase : FunctionMessage
    {

    }

    public partial class TotalPurchasedFunction : TotalPurchasedFunctionBase { }

    [Function("totalPurchased", "uint256")]
    public class TotalPurchasedFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class VestingTotalAmountFunction : VestingTotalAmountFunctionBase { }

    [Function("vestingTotalAmount", "uint256")]
    public class VestingTotalAmountFunctionBase : FunctionMessage
    {

    }

    public partial class WithdrawFundsFunction : WithdrawFundsFunctionBase { }

    [Function("withdrawFunds", "bool")]
    public class WithdrawFundsFunctionBase : FunctionMessage
    {
        [Parameter("address", "_recipient", 1)]
        public virtual string Recipient { get; set; }
    }

    public partial class WithdrawNotSoldTokensFunction : WithdrawNotSoldTokensFunctionBase { }

    [Function("withdrawNotSoldTokens", "bool")]
    public class WithdrawNotSoldTokensFunctionBase : FunctionMessage
    {
        [Parameter("address", "_recipient", 1)]
        public virtual string Recipient { get; set; }
    }

    public partial class ClaimedTokensEventDTO : ClaimedTokensEventDTOBase { }

    [Event("ClaimedTokens")]
    public class ClaimedTokensEventDTOBase : IEventDTO
    {
        [Parameter("address", "beneficiary", 1, true )]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "amountClaimed", 2, false )]
        public virtual BigInteger AmountClaimed { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }

    public partial class PurchaseTokensEventDTO : PurchaseTokensEventDTOBase { }

    [Event("PurchaseTokens")]
    public class PurchaseTokensEventDTOBase : IEventDTO
    {
        [Parameter("address", "beneficiary", 1, false )]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "tokenAmount", 2, false )]
        public virtual BigInteger TokenAmount { get; set; }
        [Parameter("uint256", "busdAmount", 3, false )]
        public virtual BigInteger BusdAmount { get; set; }
    }

    public partial class RevokedVestingEventDTO : RevokedVestingEventDTOBase { }

    [Event("RevokedVesting")]
    public class RevokedVestingEventDTOBase : IEventDTO
    {
        [Parameter("address", "beneficiary", 1, false )]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "vestedAmount", 2, false )]
        public virtual BigInteger VestedAmount { get; set; }
    }

    public partial class VestingCreatedEventDTO : VestingCreatedEventDTOBase { }

    [Event("VestingCreated")]
    public class VestingCreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "beneficiary", 1, true )]
        public virtual string Beneficiary { get; set; }
        [Parameter("uint256", "amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class VestingFundedEventDTO : VestingFundedEventDTOBase { }

    [Event("VestingFunded")]
    public class VestingFundedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "totalTokens", 1, false )]
        public virtual BigInteger TotalTokens { get; set; }
    }

    public partial class BUSDOutputDTO : BUSDOutputDTOBase { }

    [FunctionOutput]
    public class BUSDOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class CliffOutputDTO : CliffOutputDTOBase { }

    [FunctionOutput]
    public class CliffOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DurationOutputDTO : DurationOutputDTOBase { }

    [FunctionOutput]
    public class DurationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class GetVestingOutputDTO : GetVestingOutputDTOBase { }

    [FunctionOutput]
    public class GetVestingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
        [Parameter("bool", "", 3)]
        public virtual bool ReturnValue3 { get; set; }
        [Parameter("bool", "", 4)]
        public virtual bool ReturnValue4 { get; set; }
    }

    public partial class GetVestingCountOutputDTO : GetVestingCountOutputDTOBase { }

    [FunctionOutput]
    public class GetVestingCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class HardCapOutputDTO : HardCapOutputDTOBase { }

    [FunctionOutput]
    public class HardCapOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PriceOutputDTO : PriceOutputDTOBase { }

    [FunctionOutput]
    public class PriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class RevocableOutputDTO : RevocableOutputDTOBase { }

    [FunctionOutput]
    public class RevocableOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class SaleEndTimeOutputDTO : SaleEndTimeOutputDTOBase { }

    [FunctionOutput]
    public class SaleEndTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SaleStartTimeOutputDTO : SaleStartTimeOutputDTOBase { }

    [FunctionOutput]
    public class SaleStartTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class SlicePeriodSecondsOutputDTO : SlicePeriodSecondsOutputDTOBase { }

    [FunctionOutput]
    public class SlicePeriodSecondsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SoftCapOutputDTO : SoftCapOutputDTOBase { }

    [FunctionOutput]
    public class SoftCapOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class StartOutputDTO : StartOutputDTOBase { }

    [FunctionOutput]
    public class StartOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class TokenOutputDTO : TokenOutputDTOBase { }

    [FunctionOutput]
    public class TokenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalBUSDReceivedOutputDTO : TotalBUSDReceivedOutputDTOBase { }

    [FunctionOutput]
    public class TotalBUSDReceivedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalClaimableAmountOutputDTO : TotalClaimableAmountOutputDTOBase { }

    [FunctionOutput]
    public class TotalClaimableAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalPurchasedOutputDTO : TotalPurchasedOutputDTOBase { }

    [FunctionOutput]
    public class TotalPurchasedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class VestingTotalAmountOutputDTO : VestingTotalAmountOutputDTOBase { }

    [FunctionOutput]
    public class VestingTotalAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
