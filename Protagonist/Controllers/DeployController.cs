using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
using Protagonist.Services;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Protagonist.Models;
using Protagonist.Providers;
using Protagonist.Validation;
using ProtagonistPad.Contracts.Contracts.ProtagonistPad.ContractDefinition;

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
    
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> DeployProjectViaData(int id)
    {
        var project = await _projectProvider.GetById(id);
        if (project == null)
        {
            return BadRequest("could not find project");
        }
        var validator = new DeployingValidator();
        var result = await validator.ValidateAsync(project);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        var account = new Account(_chainDataService.PrivateKey, 5);
        var web3 = new Web3(account, _chainDataService.Url);
        var proLaunchpadDeployment = new ProtagonistPadDeployment(ProtagonistPadDeploymentBase.Bytecode)
        {
            FromAddress = account.Address,
            SoftCap = project.SoftCap,
            HardCap = project.HardCap,
            Busd = _chainDataService.BusdAddress,
            TokenFounder = project.TokenOwnerAddress,
            LaunchedToken = project.TokenAddress,
            SaleStartTime = project.SaleStartTime,
            SaleEndTime = project.SaleEndTime,
            Price = (BigInteger)project.TokenPrice
        };
        var transaction = await web3.Eth.GetContractDeploymentHandler<ProtagonistPadDeployment>().SendRequestAndWaitForReceiptAsync(proLaunchpadDeployment);
        if (transaction.Succeeded())
        {
            project.ProjectDeployedAddress = transaction.ContractAddress;
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
