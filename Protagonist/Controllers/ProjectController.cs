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
    
    [HttpGet("all-projects-list")]
    public IEnumerable<ProjectModel> GetAllProjects()
    {
        var projects = _projectProvider.GetAll().Result.ToList();
        return projects;
    }
    
    [HttpGet("approved-project-list")]
    public IEnumerable<ProjectModel> GetApprovedProjects()
    {
        var approvedProjects = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Approved);
        return approvedProjects;
    }
    
    [HttpGet("rejected-projects")]
    public IEnumerable<ProjectModel> RejectedProjectList()
    {
        var rejectedProjects = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Rejected);
        return rejectedProjects;
    }
    
    [HttpGet("pending-projects")]
    public IEnumerable<ProjectModel> PendingProjectList()
    {
        var pendingProjects = _projectProvider.GetAll().Result.Where(model => model.Status == ProjectStatus.Pending);
        return pendingProjects;
    }
    
    [HttpGet("{id:int}")]
    public Task<ProjectModel?> GetById(int id)
    {
        return _projectProvider.GetById(id);
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
