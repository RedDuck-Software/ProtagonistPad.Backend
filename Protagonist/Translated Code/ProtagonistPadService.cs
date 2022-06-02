using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using ProtagonistPad.Contracts.Contracts.ProtagonistPad.ContractDefinition;
// ReSharper disable All

namespace ProtagonistPad.Contracts.Contracts.ProtagonistPad
{
    public partial class ProtagonistPadService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProtagonistPadDeployment protagonistPadDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProtagonistPadDeployment>().SendRequestAndWaitForReceiptAsync(protagonistPadDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProtagonistPadDeployment protagonistPadDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProtagonistPadDeployment>().SendRequestAsync(protagonistPadDeployment);
        }

        public static async Task<ProtagonistPadService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProtagonistPadDeployment protagonistPadDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, protagonistPadDeployment, cancellationTokenSource);
            return new ProtagonistPadService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProtagonistPadService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<bool> AllowRefundQueryAsync(AllowRefundFunction allowRefundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowRefundFunction, bool>(allowRefundFunction, blockParameter);
        }

        
        public Task<bool> AllowRefundQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowRefundFunction, bool>(null, blockParameter);
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

        public Task<string> EnableRefundRequestAsync(EnableRefundFunction enableRefundFunction)
        {
             return ContractHandler.SendRequestAsync(enableRefundFunction);
        }

        public Task<string> EnableRefundRequestAsync()
        {
             return ContractHandler.SendRequestAsync<EnableRefundFunction>();
        }

        public Task<TransactionReceipt> EnableRefundRequestAndWaitForReceiptAsync(EnableRefundFunction enableRefundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(enableRefundFunction, cancellationToken);
        }

        public Task<TransactionReceipt> EnableRefundRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<EnableRefundFunction>(null, cancellationToken);
        }

        public Task<BigInteger> GetFundsQueryAsync(GetFundsFunction getFundsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFundsFunction, BigInteger>(getFundsFunction, blockParameter);
        }

        
        public Task<BigInteger> GetFundsQueryAsync(string beneficiary, BlockParameter blockParameter = null)
        {
            var getFundsFunction = new GetFundsFunction();
                getFundsFunction.Beneficiary = beneficiary;
            
            return ContractHandler.QueryAsync<GetFundsFunction, BigInteger>(getFundsFunction, blockParameter);
        }

        public Task<BigInteger> GetReleasableAmountQueryAsync(GetReleasableAmountFunction getReleasableAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetReleasableAmountFunction, BigInteger>(getReleasableAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetReleasableAmountQueryAsync(string beneficiary, BlockParameter blockParameter = null)
        {
            var getReleasableAmountFunction = new GetReleasableAmountFunction();
                getReleasableAmountFunction.Beneficiary = beneficiary;
            
            return ContractHandler.QueryAsync<GetReleasableAmountFunction, BigInteger>(getReleasableAmountFunction, blockParameter);
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

        public Task<BigInteger> GetWithdrawableTokensAmountQueryAsync(GetWithdrawableTokensAmountFunction getWithdrawableTokensAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawableTokensAmountFunction, BigInteger>(getWithdrawableTokensAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetWithdrawableTokensAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawableTokensAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> HardCapQueryAsync(HardCapFunction hardCapFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HardCapFunction, BigInteger>(hardCapFunction, blockParameter);
        }

        
        public Task<BigInteger> HardCapQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HardCapFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> InitRequestAsync(InitFunction initFunction)
        {
             return ContractHandler.SendRequestAsync(initFunction);
        }

        public Task<string> InitRequestAsync()
        {
             return ContractHandler.SendRequestAsync<InitFunction>();
        }

        public Task<TransactionReceipt> InitRequestAndWaitForReceiptAsync(InitFunction initFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initFunction, cancellationToken);
        }

        public Task<TransactionReceipt> InitRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<InitFunction>(null, cancellationToken);
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

        public Task<string> RefundRequestAsync(RefundFunction refundFunction)
        {
             return ContractHandler.SendRequestAsync(refundFunction);
        }

        public Task<string> RefundRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RefundFunction>();
        }

        public Task<TransactionReceipt> RefundRequestAndWaitForReceiptAsync(RefundFunction refundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(refundFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RefundRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RefundFunction>(null, cancellationToken);
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

        public Task<string> SetTokenFounderRequestAsync(SetTokenFounderFunction setTokenFounderFunction)
        {
             return ContractHandler.SendRequestAsync(setTokenFounderFunction);
        }

        public Task<TransactionReceipt> SetTokenFounderRequestAndWaitForReceiptAsync(SetTokenFounderFunction setTokenFounderFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTokenFounderFunction, cancellationToken);
        }

        public Task<string> SetTokenFounderRequestAsync(string newTokenFounder)
        {
            var setTokenFounderFunction = new SetTokenFounderFunction();
                setTokenFounderFunction.NewTokenFounder = newTokenFounder;
            
             return ContractHandler.SendRequestAsync(setTokenFounderFunction);
        }

        public Task<TransactionReceipt> SetTokenFounderRequestAndWaitForReceiptAsync(string newTokenFounder, CancellationTokenSource cancellationToken = null)
        {
            var setTokenFounderFunction = new SetTokenFounderFunction();
                setTokenFounderFunction.NewTokenFounder = newTokenFounder;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTokenFounderFunction, cancellationToken);
        }

        public Task<string> SetVestingRequestAsync(SetVestingFunction setVestingFunction)
        {
             return ContractHandler.SendRequestAsync(setVestingFunction);
        }

        public Task<TransactionReceipt> SetVestingRequestAndWaitForReceiptAsync(SetVestingFunction setVestingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingFunction, cancellationToken);
        }

        public Task<string> SetVestingRequestAsync(BigInteger start, BigInteger cliff, BigInteger duration, bool revocable)
        {
            var setVestingFunction = new SetVestingFunction();
                setVestingFunction.Start = start;
                setVestingFunction.Cliff = cliff;
                setVestingFunction.Duration = duration;
                setVestingFunction.Revocable = revocable;
            
             return ContractHandler.SendRequestAsync(setVestingFunction);
        }

        public Task<TransactionReceipt> SetVestingRequestAndWaitForReceiptAsync(BigInteger start, BigInteger cliff, BigInteger duration, bool revocable, CancellationTokenSource cancellationToken = null)
        {
            var setVestingFunction = new SetVestingFunction();
                setVestingFunction.Start = start;
                setVestingFunction.Cliff = cliff;
                setVestingFunction.Duration = duration;
                setVestingFunction.Revocable = revocable;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingFunction, cancellationToken);
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

        public Task<string> TokenQueryAsync(TokenFunction tokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(tokenFunction, blockParameter);
        }

        
        public Task<string> TokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(null, blockParameter);
        }

        public Task<string> TokenFounderQueryAsync(TokenFounderFunction tokenFounderFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFounderFunction, string>(tokenFounderFunction, blockParameter);
        }

        
        public Task<string> TokenFounderQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFounderFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TotalBUSDAmountQueryAsync(TotalBUSDAmountFunction totalBUSDAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalBUSDAmountFunction, BigInteger>(totalBUSDAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalBUSDAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalBUSDAmountFunction, BigInteger>(null, blockParameter);
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

        public Task<BigInteger> TotalUnreleasedQueryAsync(TotalUnreleasedFunction totalUnreleasedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalUnreleasedFunction, BigInteger>(totalUnreleasedFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalUnreleasedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalUnreleasedFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalVestingAmountQueryAsync(TotalVestingAmountFunction totalVestingAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalVestingAmountFunction, BigInteger>(totalVestingAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalVestingAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalVestingAmountFunction, BigInteger>(null, blockParameter);
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

        public Task<string> WithdrawTokensRequestAsync(WithdrawTokensFunction withdrawTokensFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawTokensRequestAndWaitForReceiptAsync(WithdrawTokensFunction withdrawTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawTokensFunction, cancellationToken);
        }

        public Task<string> WithdrawTokensRequestAsync(string recipient, BigInteger amount)
        {
            var withdrawTokensFunction = new WithdrawTokensFunction();
                withdrawTokensFunction.Recipient = recipient;
                withdrawTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(withdrawTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawTokensRequestAndWaitForReceiptAsync(string recipient, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawTokensFunction = new WithdrawTokensFunction();
                withdrawTokensFunction.Recipient = recipient;
                withdrawTokensFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawTokensFunction, cancellationToken);
        }
    }
}
