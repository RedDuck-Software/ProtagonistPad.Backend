using Microsoft.EntityFrameworkCore;
using Protagonist.Models;

namespace Protagonist.Providers;

public class ProjectProvider : IProjectProvider
{
    private readonly ProjectsDb _projectsDb;
    private int CurrentId()
    {
        return !_projectsDb.Projects.Any()
            ? 1 : 1 + _projectsDb.Projects.AsEnumerable().MaxBy(project => project.Id)!.Id;
    }

    public ProjectProvider(ProjectsDb projectsDb)
    {
        _projectsDb = projectsDb;
    }

    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        return await _projectsDb.Projects.ToListAsync();
    }
    
    public async Task CreateProject(ProjectModel projectModel)
    {
        await _projectsDb.Projects.AddAsync(new ProjectModel(projectModel, CurrentId()));
        await _projectsDb.SaveChangesAsync();
    }
    
    public async Task<ProjectModel?> GetById(int id)
    {
        return await _projectsDb.Projects.FindAsync(id);
    }
    
    public async Task UpdateProject(ProjectModel projectModel)
    {
        var project = await _projectsDb.Projects.FindAsync(projectModel.Id);
        if(project == null) return;
        
        project.UserName = projectModel.UserName;
        project.TokenPrice = projectModel.TokenPrice;
        project.Address = projectModel.Address;
        project.Duration = project.Duration;
        project.HardCap = projectModel.HardCap;
        project.SoftCap = projectModel.SoftCap;
        project.ProjectDescription = projectModel.ProjectDescription;
        project.ProjectName = projectModel.ProjectName;
        
        await _projectsDb.SaveChangesAsync();
    }
    public async Task DeleteProject(int id)
    {
        var currentUser = await _projectsDb.Projects.FindAsync(id);
        if (currentUser == null) return;
        _projectsDb.Projects.Remove(currentUser);
        await _projectsDb.SaveChangesAsync();
    }
}
