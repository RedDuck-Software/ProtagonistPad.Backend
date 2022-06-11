using Microsoft.EntityFrameworkCore;
using Protagonist.Models;

namespace Protagonist;

public class ProjectsDb : DbContext
{
    public ProjectsDb(DbContextOptions<ProjectsDb> options) : base(options) { }
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectModel>().HasKey(i => i.Id);
    }
}