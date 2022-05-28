using Microsoft.EntityFrameworkCore;
using Protagonist.Models;

namespace Protagonist;

public class ProjectsDb : DbContext
{
    public ProjectsDb(DbContextOptions<ProjectsDb> options) : base(options) { }
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
}
