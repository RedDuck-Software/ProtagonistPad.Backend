namespace Protagonist.Models;
// ReSharper disable once MemberCanBePrivate.Global
public class ProjectModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectDescription { get; set; } = string.Empty;
    public int HardCap { get; set; } 
    public int SoftCap { get; set; }
    public int Duration { get; set; }
    public decimal TokenPrice { get; set; }
    public string DeployedAddress { get; set; } = string.Empty;
    public string TokenFounder { get; set; } = string.Empty;
    public string UserTelegram { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
    public int SaleStartTime { get; set; }
    public int SaleEndTime { get; set; }
    public ProjectModel() { }
    public ProjectModel(ProjectModel projectModel, int currentId)
    {
        Id = currentId;
        ProjectName = projectModel.UserName;
        ProjectDescription = projectModel.ProjectDescription;
        HardCap = projectModel.HardCap;
        SoftCap = projectModel.SoftCap;
        Duration = projectModel.Duration;
        TokenPrice = projectModel.TokenPrice;
        DeployedAddress = projectModel.DeployedAddress;
        TokenFounder = projectModel.TokenFounder;
        UserTelegram = projectModel.UserTelegram;
        SaleStartTime = projectModel.SaleStartTime;
        SaleEndTime = projectModel.SaleEndTime;
        Status = ProjectStatus.Pending;
    }
}
//1655274256
//1654074256
public enum ProjectStatus
{
    Approved,
    Rejected,
    Pending,
    Error
}
