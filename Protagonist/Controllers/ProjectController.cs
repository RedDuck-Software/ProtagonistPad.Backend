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

    [HttpGet("approved-project-list")]
    public IEnumerable<ProjectModel> GetApprovedProjects()
    {
        var approvedProjects = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Approved);
        return approvedProjects;
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
    public Task RejectProject(int id)
    {
        return _projectProvider.RejectProject(id);
    }
}
