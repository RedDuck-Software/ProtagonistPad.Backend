using Protagonist.Models;

namespace Protagonist.Providers;

public interface IProjectProvider
{
    public Task<IEnumerable<ProjectModel>> GetAll();
    public Task<ProjectModel?> GetById(int id);
    public Task ApplyProject(ProjectModel projectModel);
    public Task UpdateProject(ProjectModel projectModel);
    public Task DeleteProject(int id);

}
