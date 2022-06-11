using Microsoft.EntityFrameworkCore;
using Protagonist.Models;
using Protagonist.Services;

namespace Protagonist.Providers;

public class ProjectProvider : IProjectProvider
{
    private readonly ProjectsDb _projectsDb;

    public ProjectProvider(ProjectsDb projectsDb)
    {
        _projectsDb = projectsDb;
    }
    
    public async Task<ProjectModel?> GetById(int id)
    {
        return await _projectsDb.Projects.FindAsync(id);
    }
    
    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        return await _projectsDb.Projects.ToListAsync();
    }
    
    public Task<IEnumerable<ProjectModel>> GetApprovedProjects()
    {
        return _projectsDb.Projects.AsNoTracking().Where(i => i.Status == ProjectStatus.Approved)
            .ToListAsync().ContinueWith(i => (IEnumerable<ProjectModel>)i.Result);
    }
    
    public Task ApplyProject(ProjectModel projectModel) // TODO: DB Increment Primary Key Id 
    {
        projectModel.SaleStartTime = new DateTimeOffset(projectModel.SaleStartDateTime).ToUnixTimeSeconds();
        projectModel.SaleEndTime = new DateTimeOffset(projectModel.SaleEndDateTime).ToUnixTimeSeconds();
        projectModel.Status = ProjectStatus.Pending;

        _projectsDb.Attach(projectModel);
        _projectsDb.Entry(projectModel).State = EntityState.Added;

        return _projectsDb.SaveChangesAsync();
    }
    
    public Task UpdateProject(ProjectModel projectModel)
    {
        _projectsDb.Attach(projectModel);
        _projectsDb.Entry(projectModel).State = EntityState.Modified;
        return _projectsDb.SaveChangesAsync();
    }

    public async Task RejectProject(int id)
    {
        var project = await _projectsDb.Projects.FindAsync(id);

        if (project == null) return; // TODO: add logger / change without read (logger will not be relevant if we update without reading)

        project.Status = ProjectStatus.Rejected; // TODO: Update without reading 

        await _projectsDb.SaveChangesAsync();
    }
    
    public async Task DeleteProject(int id)
    {
        var project = await _projectsDb.Projects.FindAsync(id); // TODO: Delete without reading  
        if (project == null) return;
        _projectsDb.Projects.Remove(project);
        await _projectsDb.SaveChangesAsync();
    }
}
