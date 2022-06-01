using Example.Contracts.ProLaunchpad.ContractDefinition;
using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
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
    public async Task<ActionResult<string>> DeployProjectViaData(int id)
    {
        var project = _projectProvider.GetById(id).Result;
        if (project == null)
        {
            return BadRequest();
        }
        var account = new Account(_chainDataService.PrivateKey, 5);
        var web3 = new Web3(account, _chainDataService.Url);
        var proLaunchpadDeployment = new ProLaunchpadDeployment(ProLaunchpadDeploymentBase.Bytecode)
        {
            FromAddress = account.Address,
            SoftCap = project.SoftCap,
            HardCap = project.HardCap,
            BUSD = _chainDataService.BusdAddress,
            LaunchedToken = project.Address,
            SaleStartTime = project.SaleStartTime,
            SaleEndTime = project.SaleEndTime, 
        };
        var transaction = await web3.Eth.GetContractDeploymentHandler<ProLaunchpadDeployment>().SendRequestAndWaitForReceiptAsync(proLaunchpadDeployment);
        if (transaction.Succeeded())
        {
            project.Address = transaction.ContractAddress;
            project.Status = ProjectStatus.Approved;
            await _projectProvider.UpdateProject(project);
        }
        else
        {
            project.Status = ProjectStatus.Error;
            await _projectProvider.UpdateProject(project);
        }
        return transaction.ContractAddress;
    }
}
