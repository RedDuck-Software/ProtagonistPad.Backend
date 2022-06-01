using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Example.Contracts.ProLaunchpad.ContractDefinition;
// ReSharper disable InconsistentNaming

namespace Example.Contracts.ProLaunchpad
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public partial class ProLaunchpadService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProLaunchpadDeployment proLaunchpadDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProLaunchpadDeployment>().SendRequestAndWaitForReceiptAsync(proLaunchpadDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProLaunchpadDeployment proLaunchpadDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProLaunchpadDeployment>().SendRequestAsync(proLaunchpadDeployment);
        }

        public static async Task<ProLaunchpadService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProLaunchpadDeployment proLaunchpadDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, proLaunchpadDeployment, cancellationTokenSource);
            return new ProLaunchpadService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProLaunchpadService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> BUSDQueryAsync(BUSDFunction bUSDFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BUSDFunction, string>(bUSDFunction, blockParameter);
        }

        
        public Task<string> BUSDQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BUSDFunction, string>(null, blockParameter);
        }

        public Task<string> ClaimVestedTokensRequestAsync(ClaimVestedTokensFunction claimVestedTokensFunction)
        {
             return ContractHandler.SendRequestAsync(claimVestedTokensFunction);
        }

        public Task<TransactionReceipt> ClaimVestedTokensRequestAndWaitForReceiptAsync(ClaimVestedTokensFunction claimVestedTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimVestedTokensFunction, cancellationToken);
        }

        public Task<string> ClaimVestedTokensRequestAsync(string beneficiary, BigInteger amount)
        {
            var claimVestedTokensFunction = new ClaimVestedTokensFunction();
                claimVestedTokensFunction.Beneficiary = beneficiary;
                claimVestedTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(claimVestedTokensFunction);
        }

        public Task<TransactionReceipt> ClaimVestedTokensRequestAndWaitForReceiptAsync(string beneficiary, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var claimVestedTokensFunction = new ClaimVestedTokensFunction();
                claimVestedTokensFunction.Beneficiary = beneficiary;
                claimVestedTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimVestedTokensFunction, cancellationToken);
        }

        public Task<BigInteger> CliffQueryAsync(CliffFunction cliffFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CliffFunction, BigInteger>(cliffFunction, blockParameter);
        }

        
        public Task<BigInteger> CliffQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CliffFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> DurationQueryAsync(DurationFunction durationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(durationFunction, blockParameter);
        }

        
        public Task<BigInteger> DurationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> FundVestingRequestAsync(FundVestingFunction fundVestingFunction)
        {
             return ContractHandler.SendRequestAsync(fundVestingFunction);
        }

        public Task<TransactionReceipt> FundVestingRequestAndWaitForReceiptAsync(FundVestingFunction fundVestingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fundVestingFunction, cancellationToken);
        }

        public Task<string> FundVestingRequestAsync(BigInteger totalTokens)
        {
            var fundVestingFunction = new FundVestingFunction();
                fundVestingFunction.TotalTokens = totalTokens;
            
             return ContractHandler.SendRequestAsync(fundVestingFunction);
        }

        public Task<TransactionReceipt> FundVestingRequestAndWaitForReceiptAsync(BigInteger totalTokens, CancellationTokenSource cancellationToken = null)
        {
            var fundVestingFunction = new FundVestingFunction();
                fundVestingFunction.TotalTokens = totalTokens;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fundVestingFunction, cancellationToken);
        }

        public Task<GetVestingOutputDTO> GetVestingQueryAsync(GetVestingFunction getVestingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVestingFunction, GetVestingOutputDTO>(getVestingFunction, blockParameter);
        }

        public Task<GetVestingOutputDTO> GetVestingQueryAsync(string beneficiary, BlockParameter blockParameter = null)
        {
            var getVestingFunction = new GetVestingFunction();
                getVestingFunction.Beneficiary = beneficiary;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetVestingFunction, GetVestingOutputDTO>(getVestingFunction, blockParameter);
        }

        public Task<BigInteger> GetVestingCountQueryAsync(GetVestingCountFunction getVestingCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetVestingCountFunction, BigInteger>(getVestingCountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetVestingCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetVestingCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> HardCapQueryAsync(HardCapFunction hardCapFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HardCapFunction, BigInteger>(hardCapFunction, blockParameter);
        }

        
        public Task<BigInteger> HardCapQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HardCapFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> PriceQueryAsync(PriceFunction priceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceFunction, BigInteger>(priceFunction, blockParameter);
        }

        
        public Task<BigInteger> PriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> PurchaseTokensRequestAsync(PurchaseTokensFunction purchaseTokensFunction)
        {
             return ContractHandler.SendRequestAsync(purchaseTokensFunction);
        }

        public Task<TransactionReceipt> PurchaseTokensRequestAndWaitForReceiptAsync(PurchaseTokensFunction purchaseTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(purchaseTokensFunction, cancellationToken);
        }

        public Task<string> PurchaseTokensRequestAsync(string beneficiary, BigInteger amount)
        {
            var purchaseTokensFunction = new PurchaseTokensFunction();
                purchaseTokensFunction.Beneficiary = beneficiary;
                purchaseTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(purchaseTokensFunction);
        }

        public Task<TransactionReceipt> PurchaseTokensRequestAndWaitForReceiptAsync(string beneficiary, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var purchaseTokensFunction = new PurchaseTokensFunction();
                purchaseTokensFunction.Beneficiary = beneficiary;
                purchaseTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(purchaseTokensFunction, cancellationToken);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<bool> RevocableQueryAsync(RevocableFunction revocableFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RevocableFunction, bool>(revocableFunction, blockParameter);
        }

        
        public Task<bool> RevocableQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RevocableFunction, bool>(null, blockParameter);
        }

        public Task<string> RevokeRequestAsync(RevokeFunction revokeFunction)
        {
             return ContractHandler.SendRequestAsync(revokeFunction);
        }

        public Task<TransactionReceipt> RevokeRequestAndWaitForReceiptAsync(RevokeFunction revokeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeFunction, cancellationToken);
        }

        public Task<string> RevokeRequestAsync(string beneficiary)
        {
            var revokeFunction = new RevokeFunction();
                revokeFunction.Beneficiary = beneficiary;
            
             return ContractHandler.SendRequestAsync(revokeFunction);
        }

        public Task<TransactionReceipt> RevokeRequestAndWaitForReceiptAsync(string beneficiary, CancellationTokenSource cancellationToken = null)
        {
            var revokeFunction = new RevokeFunction();
                revokeFunction.Beneficiary = beneficiary;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeFunction, cancellationToken);
        }

        public Task<BigInteger> SaleEndTimeQueryAsync(SaleEndTimeFunction saleEndTimeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SaleEndTimeFunction, BigInteger>(saleEndTimeFunction, blockParameter);
        }

        
        public Task<BigInteger> SaleEndTimeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SaleEndTimeFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> SaleStartTimeQueryAsync(SaleStartTimeFunction saleStartTimeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SaleStartTimeFunction, BigInteger>(saleStartTimeFunction, blockParameter);
        }

        
        public Task<BigInteger> SaleStartTimeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SaleStartTimeFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> SetVestingRequestAsync(SetVestingFunction setVestingFunction)
        {
             return ContractHandler.SendRequestAsync(setVestingFunction);
        }

        public Task<TransactionReceipt> SetVestingRequestAndWaitForReceiptAsync(SetVestingFunction setVestingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingFunction, cancellationToken);
        }

        public Task<string> SetVestingRequestAsync(BigInteger start, BigInteger cliff, BigInteger duration, BigInteger slicePeriodSeconds, bool revocable)
        {
            var setVestingFunction = new SetVestingFunction();
                setVestingFunction.Start = start;
                setVestingFunction.Cliff = cliff;
                setVestingFunction.Duration = duration;
                setVestingFunction.SlicePeriodSeconds = slicePeriodSeconds;
                setVestingFunction.Revocable = revocable;
            
             return ContractHandler.SendRequestAsync(setVestingFunction);
        }

        public Task<TransactionReceipt> SetVestingRequestAndWaitForReceiptAsync(BigInteger start, BigInteger cliff, BigInteger duration, BigInteger slicePeriodSeconds, bool revocable, CancellationTokenSource cancellationToken = null)
        {
            var setVestingFunction = new SetVestingFunction();
                setVestingFunction.Start = start;
                setVestingFunction.Cliff = cliff;
                setVestingFunction.Duration = duration;
                setVestingFunction.SlicePeriodSeconds = slicePeriodSeconds;
                setVestingFunction.Revocable = revocable;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingFunction, cancellationToken);
        }

        public Task<BigInteger> SlicePeriodSecondsQueryAsync(SlicePeriodSecondsFunction slicePeriodSecondsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SlicePeriodSecondsFunction, BigInteger>(slicePeriodSecondsFunction, blockParameter);
        }

        
        public Task<BigInteger> SlicePeriodSecondsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SlicePeriodSecondsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> SoftCapQueryAsync(SoftCapFunction softCapFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SoftCapFunction, BigInteger>(softCapFunction, blockParameter);
        }

        
        public Task<BigInteger> SoftCapQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SoftCapFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> StartQueryAsync(StartFunction startFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StartFunction, BigInteger>(startFunction, blockParameter);
        }

        
        public Task<BigInteger> StartQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StartFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> StartPurchaseRequestAsync(StartPurchaseFunction startPurchaseFunction)
        {
             return ContractHandler.SendRequestAsync(startPurchaseFunction);
        }

        public Task<TransactionReceipt> StartPurchaseRequestAndWaitForReceiptAsync(StartPurchaseFunction startPurchaseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startPurchaseFunction, cancellationToken);
        }

        public Task<string> StartPurchaseRequestAsync(BigInteger price)
        {
            var startPurchaseFunction = new StartPurchaseFunction();
                startPurchaseFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(startPurchaseFunction);
        }

        public Task<TransactionReceipt> StartPurchaseRequestAndWaitForReceiptAsync(BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var startPurchaseFunction = new StartPurchaseFunction();
                startPurchaseFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startPurchaseFunction, cancellationToken);
        }

        public Task<string> TokenQueryAsync(TokenFunction tokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(tokenFunction, blockParameter);
        }

        
        public Task<string> TokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TotalBUSDReceivedQueryAsync(TotalBUSDReceivedFunction totalBUSDReceivedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalBUSDReceivedFunction, BigInteger>(totalBUSDReceivedFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalBUSDReceivedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalBUSDReceivedFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalClaimableAmountQueryAsync(TotalClaimableAmountFunction totalClaimableAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalClaimableAmountFunction, BigInteger>(totalClaimableAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalClaimableAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalClaimableAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalPurchasedQueryAsync(TotalPurchasedFunction totalPurchasedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalPurchasedFunction, BigInteger>(totalPurchasedFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalPurchasedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalPurchasedFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<BigInteger> VestingTotalAmountQueryAsync(VestingTotalAmountFunction vestingTotalAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VestingTotalAmountFunction, BigInteger>(vestingTotalAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> VestingTotalAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VestingTotalAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> WithdrawFundsRequestAsync(WithdrawFundsFunction withdrawFundsFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFundsFunction);
        }

        public Task<TransactionReceipt> WithdrawFundsRequestAndWaitForReceiptAsync(WithdrawFundsFunction withdrawFundsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFundsFunction, cancellationToken);
        }

        public Task<string> WithdrawFundsRequestAsync(string recipient)
        {
            var withdrawFundsFunction = new WithdrawFundsFunction();
                withdrawFundsFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(withdrawFundsFunction);
        }

        public Task<TransactionReceipt> WithdrawFundsRequestAndWaitForReceiptAsync(string recipient, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFundsFunction = new WithdrawFundsFunction();
                withdrawFundsFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFundsFunction, cancellationToken);
        }

        public Task<string> WithdrawNotSoldTokensRequestAsync(WithdrawNotSoldTokensFunction withdrawNotSoldTokensFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawNotSoldTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawNotSoldTokensRequestAndWaitForReceiptAsync(WithdrawNotSoldTokensFunction withdrawNotSoldTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawNotSoldTokensFunction, cancellationToken);
        }

        public Task<string> WithdrawNotSoldTokensRequestAsync(string recipient)
        {
            var withdrawNotSoldTokensFunction = new WithdrawNotSoldTokensFunction();
                withdrawNotSoldTokensFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(withdrawNotSoldTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawNotSoldTokensRequestAndWaitForReceiptAsync(string recipient, CancellationTokenSource cancellationToken = null)
        {
            var withdrawNotSoldTokensFunction = new WithdrawNotSoldTokensFunction();
                withdrawNotSoldTokensFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawNotSoldTokensFunction, cancellationToken);
        }
    }
}
