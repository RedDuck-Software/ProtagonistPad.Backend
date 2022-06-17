using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Protagonist.Models;
using System.Text.Json;
using Xunit.Abstractions;

namespace ProtagonistTests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public class ProtagonistTests : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ProtagonistTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca782174d", 1, 2000, 1000, "Project Description", 150000, 200000, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 1, "@name")]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca782174d", 2, 3000000, 1000000, "Br-t-t-t Description", 3, 4, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 5, "@name")]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca782174d", 3, 2, 1, "Some description", 150000, 200000, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 10, "@name")]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca782174d", 4, 200000000, 100000000, "Description", 1, 2, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 200, "@name")]
    public async Task POST_ProjectModel_Responds_OK(string address, int duration, int hardCap, int softCap, string projectDescription, int saleStartTime,
        int saleEndTime, string projectName, string tokenFounder, int tokenPrice, string userTelegram)
    {
        await using var application = new ProtagonistPlayground();
        using var client = application.CreateClient();
        var model = new ProjectModel()
        {
            Duration = duration,
            HardCap = hardCap,
            SoftCap = softCap,
            ProjectDescription = projectDescription,
            SaleStartTime = saleStartTime,
            SaleEndTime = saleEndTime,
            ProjectName = projectName,
            TokenPrice = tokenPrice,
            UserTelegram = userTelegram
        };
        using var jsonContent = new StringContent(JsonSerializer.Serialize(model));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        using var response = await client.PostAsync("api/project/apply-project", jsonContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Theory]
    [InlineData("0x4330202d2da5ca782174d", 1, 2000, 1000, "Project Description", 150000, 200000, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 1, "@name")]
    [InlineData("0eddd433022c4066558e7a32d850f02d2da5ca782174d", 1, 2000, 1000, "Project Description", 150000, 200000, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 1, "@name")]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca774d", 1, 1000, 10000, "Project Description", 150000, 200000, "SomeName", "0x433022c4066558e7a32d850f02d2da5ca782174d", 1, "@name")]
    [InlineData("0x433022c4066558e7a32d850f02d2da5ca782174d", 1, 2000, 1000, "Project Description", 150000, 200000, "SomeName", "0x4022c7a32d02d2da5ca782174d", 1, "@name")]
    public async Task POST_ProjectModel_Responds_BadRequest(string address, int duration, int hardCap, int softCap, string projectDescription, int saleStartTime,
        int saleEndTime, string projectName, string tokenFounder, int tokenPrice, string userTelegram)
    {
        await using var application = new ProtagonistPlayground();
        using var client = application.CreateClient();
        var model = new ProjectModel()
        {
            Duration = duration,
            HardCap = hardCap,
            SoftCap = softCap,
            ProjectDescription = projectDescription,
            SaleStartTime = saleStartTime,
            SaleEndTime = saleEndTime,
            ProjectName = projectName,
            TokenPrice = tokenPrice,
            UserTelegram = userTelegram
        };
        using var jsonContent = new StringContent(JsonSerializer.Serialize(model));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        using var response = await client.PostAsync("api/project/apply-project", jsonContent);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task PUT_ProjectModel_Responds_Unauthorized()
    {
        await using var application = new ProtagonistPlayground();
        using var client = application.CreateClient();
        using var jsonContent = new StringContent(JsonSerializer.Serialize(new ProjectModel()));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        using var response = await client.PutAsync("api/project/update-project", jsonContent);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task POST_ProjectModel_Responds_BadRequestA()
    {
        await using var application = new ProtagonistPlayground();
        using var client = application.CreateClient();
        var project = new ProjectModel();
        using var jsonContent = new StringContent(JsonSerializer.Serialize(project));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        _testOutputHelper.WriteLine(jsonContent.ToString());
        using var response = await client.PostAsync("api/project/apply-project", jsonContent);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
