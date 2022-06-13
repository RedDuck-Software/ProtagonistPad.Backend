using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protagonist.Models;
using Protagonist.Providers;
using Protagonist.Validation;

namespace Protagonist.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectProvider _projectProvider;
    public ProjectController(ProjectProvider projectProvider)
    {
        _projectProvider = projectProvider;
    }
    
    [HttpGet("project-list"), Authorize(Roles = "Admin")]
    public IEnumerable<ProjectModel> GetProjects()
    {
        var approvedProjects = _projectProvider.GetAll().Result;
        return approvedProjects;
    }

    [HttpGet("token-image/{id:int}")]
    public async Task<ActionResult> GetTokenImage(int id)
    {
        var project = await _projectProvider.GetById(id);
        if (project == null)
        {
            return BadRequest("Project doesn't exist");
        }
        return Ok(project.TokenImage);
    }

    [HttpGet("approved-project-list")]
    public IEnumerable<ProjectModel> GetApprovedProjects()
    {
        var approvedProjects = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Approved);
        return approvedProjects;
    }
    
    [HttpGet("get/{id:int}")]
    public ProjectModel GetProjectById(int id)
    {
        var project = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Approved).ToList()[0];
        return project;
    }
    
    
    [HttpPost("apply-project")]
    public async Task<ActionResult> ApplyProject(ProjectModel projectModel)
    {
        var validator = new ProjectModelValidator();
        var result = await validator.ValidateAsync(projectModel);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _projectProvider.ApplyProject(projectModel);
        return Ok();
    }

    [HttpPut("update-project"), Authorize(Roles="Admin")]
    public Task Update(ProjectModel projectModel)
    {
        return Task.FromResult(Ok(_projectProvider.UpdateProject(projectModel)));
    }
    
    [HttpPost("reject-project/{id:int}"), Authorize(Roles="Admin")]
    public async Task<IActionResult> RejectProject(int id)
    {
        var project = await _projectProvider.GetById(id);
        if (project == null)
        {
            return BadRequest("Project doesn't exist");
        }
        await _projectProvider.RejectProject(id);
        return Ok();
    }
}
