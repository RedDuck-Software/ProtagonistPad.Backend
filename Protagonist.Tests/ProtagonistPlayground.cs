using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protagonist;

namespace ProtagonistTests;

public class ProtagonistPlayground : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public ProtagonistPlayground(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<ProjectsDb>()
                    .UseInMemoryDatabase("Tests")
                    .UseApplicationServiceProvider(sp).Options;
            });
        });
        return base.CreateHost(builder);
    }
}
