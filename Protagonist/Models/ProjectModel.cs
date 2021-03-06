using System.Globalization;
using Protagonist.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Protagonist.Models;
// ReSharper disable once MemberCanBePrivate.Global
public class ProjectModel
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserTelegram { get; set; } = string.Empty;
    public string TokenOwnerAddress { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectDescription { get; set; } = string.Empty;
    [SwaggerSchema(ReadOnly = true)]
    public string ProjectDeployedAddress { get; set; } = string.Empty;
    public string TokenImage { get; set; }
    public int HardCap { get; set; } 
    public int SoftCap { get; set; }
    public int Duration { get; set; }
    public decimal TokenPrice { get; set; }
    public string TokenName { get; set; }
    public string TokenSignature { get; set; }
    public string TokenAddress { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
    [SwaggerSchema(ReadOnly = true)]
    public long SaleStartTime { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public long SaleEndTime { get; set; }
    public DateTime SaleStartDateTime { get; set; }
    public DateTime SaleEndDateTime { get; set; }

    public ProjectModel() { }
    public ProjectModel(ProjectModel projectModel, int currentId)
    {
        Id = currentId;
        ProjectName = projectModel.ProjectName;
        TokenImage = projectModel.TokenImage;
        ProjectDescription = projectModel.ProjectDescription;
        HardCap = projectModel.HardCap;
        SoftCap = projectModel.SoftCap;
        Duration = projectModel.Duration;
        TokenPrice = projectModel.TokenPrice;
        TokenAddress = projectModel.TokenAddress;
        TokenOwnerAddress = projectModel.TokenOwnerAddress;
        UserName = projectModel.UserName;
        TokenSignature = projectModel.TokenSignature;
        TokenName = projectModel.TokenName;
        UserTelegram = projectModel.UserTelegram;
        SaleStartDateTime = projectModel.SaleStartDateTime;
        SaleEndDateTime = projectModel.SaleEndDateTime;
        SaleStartTime = ConverterService.DateTimeToTimeStamp(SaleStartDateTime);
        SaleEndTime = ConverterService.DateTimeToTimeStamp(SaleEndDateTime);
        Status = ProjectStatus.Pending;
    }
}

public enum ProjectStatus
{
    Approved,
    Rejected,
    Pending,
    Error
}
