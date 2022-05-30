using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
using Protagonist.Services;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Protagonist.Providers;
using Protagonist.Translated_Code;

namespace Protagonist.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeployController : ControllerBase
{
    private readonly ChainDataService _chainDataService;
    private readonly ProjectProvider _projectProvider;
    public DeployController(ChainDataService chainDataService, ProjectProvider projectProvider)
    {
        _chainDataService = chainDataService;
        _projectProvider = projectProvider;
    }
    [HttpPost("deploy-project-by-abi-bytecode")]
    public async Task<IActionResult> DeployProject(int id, string abi, string bytecode)
    {
        var url = _chainDataService.Url;
        var privateKey = _chainDataService.PrivateKey;
        var chainId = _chainDataService.ChainId;
        var account = new Account(privateKey, chainId);
        var web3 = new Web3(new Account(privateKey, chainId), url);
        var senderAddress = account.Address;
        TransactionReceipt receipt;
        try
        {
            var estimateGas = await web3.Eth.DeployContract.EstimateGasAsync(abi, bytecode, senderAddress);
            receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi,
                bytecode, senderAddress, estimateGas);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
        Console.WriteLine("Contract deployed at address: " + receipt.ContractAddress);
        Console.WriteLine(receipt.ContractAddress);

        var deployedProject = await _projectProvider.GetById(id);
        if (deployedProject == null) return BadRequest();
        
        deployedProject.Address = receipt.ContractAddress;
        deployedProject.Status = true;
        await _projectProvider.UpdateProject(deployedProject);
        return Ok();
    }

    [HttpPost()]
    public async Task DeployProjectViaData(string addressFrom, string gas, decimal valueAmount, decimal busd,
        string launchedToken, decimal hardCap, decimal softCap)
    {
        var url = _chainDataService.Url;
        var privateKey = _chainDataService.PrivateKey;
        var chainId = _chainDataService.ChainId;
        var account = new Account(privateKey, chainId);
        var web3 = new Web3(new Account(privateKey, chainId), url);
        var senderAddress = account.Address;
        await web3.Eth.DeployContract.SendRequestAsync(ProLaunchpad.ABI, ProLaunchpad.BYTE_CODE, addressFrom, gas, valueAmount, busd,
            launchedToken, hardCap, softCap, DateTime.Now, DateTime.Now);
    }
}
