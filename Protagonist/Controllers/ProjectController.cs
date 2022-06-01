using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protagonist.Models;
using Protagonist.Providers;

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
    
    [HttpGet("project-list")]
    public Task<IEnumerable<ProjectModel>> GetAllProjects()
    {
        return _projectProvider.GetAll();
    }
    [HttpGet("approved-project-list")]
    public Task<IEnumerable<ProjectModel>> GetApprovedProjects()
    {
        return _projectProvider.GetApprovedProjects();
    }
    
    [HttpGet("projects-count")]
    public int ProjectsCount()
    {
        return _projectProvider.GetAll().Result.Count();
    }
    
    [HttpGet("approved-projects-count")]
    public int ApprovedProjectsCount()
    {
        return _projectProvider.GetApprovedProjects().Result.Count();
    }

    [HttpGet("{id:int}")] //from all projects
    public Task<ProjectModel?> GetById(int id)
    {
        return _projectProvider.GetById(id);
    }
    
    [HttpPost("create-project")]
    public Task ApplyProject(ProjectModel projectModel)
    {
        return _projectProvider.ApplyProject(projectModel);
    }

    [HttpPut("update-project"), Authorize(Roles="Admin")]
    public Task Update(ProjectModel projectModel)
    {
        return _projectProvider.UpdateProject(projectModel);
    }
    
    [HttpDelete("{id:int}"), Authorize(Roles="Admin")]
    public Task Delete(int id)
    {
        return _projectProvider.DeleteProject(id);
    }

    [HttpPost("{id:int}"), Authorize(Roles="Admin")]
    public Task RejectProject(int id)
    {
        return _projectProvider.RejectProject(id);
    }
}
