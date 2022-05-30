namespace Protagonist.Models;
// ReSharper disable once MemberCanBePrivate.Global
public class ProjectModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public int HardCap { get; set; }
    public int SoftCap { get; set; }
    public int Duration { get; set; }
    public decimal TokenPrice { get; set; }
    public string? Address { get; set; }
    public string? UserTelegram { get; set; }
    public bool? Status { get; set; } = false;
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
        Address = projectModel.Address;
        UserTelegram = projectModel.UserTelegram;
        Status = false;
    }
}
