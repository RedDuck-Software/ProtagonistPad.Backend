using Example.Contracts.ProLaunchpad.ContractDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Protagonist.Services;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Protagonist.Models;
using Protagonist.Providers;

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
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<string>> DeployProjectViaData(int id, int softCap, int hardCap, string busd, string launchedToken, int saleStartTime, int saleEndTime, int gas)
    {
        var account = new Account(_chainDataService.PrivateKey, Chain.Rinkeby);
        var web3 = new Web3(account, _chainDataService.Url);
        if (softCap >= hardCap || busd[1] != 'x' || launchedToken[1] != 'x' || saleEndTime <= saleStartTime || busd.Length != 42 || launchedToken.Length != 42)
        {
            return BadRequest("Incorrect data");
        }
        var proLaunchpadDeployment = new ProLaunchpadDeployment(ProLaunchpadDeploymentBase.BYTECODE)
        {
            FromAddress = account.Address,
            SoftCap = softCap,
            HardCap = hardCap,
            BUSD = busd,
            LaunchedToken = launchedToken,
            SaleStartTime = saleStartTime,
            SaleEndTime = saleEndTime,
            Gas = gas
        };
        var transaction = await web3.Eth.GetContractDeploymentHandler<ProLaunchpadDeployment>().SendRequestAndWaitForReceiptAsync(proLaunchpadDeployment);
        if (transaction.Succeeded())
        {
            var project = await _projectProvider.GetById(id);
            if (project == null)
            {
                project = new ProjectModel
                    {
                        Address = transaction.ContractAddress, Id = id, ProjectName = "project" + id, HardCap = hardCap, SoftCap = softCap
                    };
                await _projectProvider.CreateProject(project);
            }
            project.Address = transaction.ContractAddress;
            project.Status = true;
            await _projectProvider.UpdateProject(project);
        }
        return transaction.ContractAddress;
    }
}
//0x9907a0cf64ec9fbf6ed8fd4971090de88222a9ac
//1654022283
//1654122283
