using Protagonist.Models;

namespace Protagonist.Providers;

public interface IProjectProvider
{
    public Task RejectProject(int id);
    public Task DeleteProject(int id);
    public Task<ProjectModel> GetById(int id);
    public Task<IEnumerable<ProjectModel>> GetAll();
    public Task<IEnumerable<ProjectModel>> GetApprovedProjects();
    public Task ApplyProject(ProjectModel projectModel);
    public Task UpdateProject(ProjectModel projectModel);
}
