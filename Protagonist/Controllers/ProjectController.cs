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
    public Task<IEnumerable<ProjectModel>> Get()
    {
        return _projectProvider.GetAll();
    }

    [HttpGet("count")]
    public int ProjectsCount()
    {
        return _projectProvider.GetAll().Result.Count();
    }

    [HttpGet("{id:int}")]
    public Task<ProjectModel> Get(int id)
    {
        return _projectProvider.GetById(id);
    }
    [HttpPost("create-project")]
    public Task Create(ProjectModel projectModel)
    {
        return _projectProvider.CreateProject(projectModel);
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
}
