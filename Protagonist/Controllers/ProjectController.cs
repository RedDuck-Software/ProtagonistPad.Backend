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
    
    [HttpGet]
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
    public Task Get(int id)
    {
        return _projectProvider.GetById(id);
    }
    [HttpPost, Authorize(Roles="Admin")]
    public Task Create(ProjectModel projectModel)
    {
        return _projectProvider.CreateProject(projectModel);
    }

    [HttpPut, Authorize(Roles="Admin")]
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
