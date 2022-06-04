using FluentValidation;
using Protagonist.Models;

namespace Protagonist.Validation;

public class ProjectModelValidator : AbstractValidator<ProjectModel>
{
    public ProjectModelValidator()
    {
        RuleFor(project => project.DeployedAddress.Length).Equal(42).WithMessage("DeployedAddress length must be 42");
        RuleFor(project => project.ProjectName).NotEmpty().WithMessage("Project name can't be empty");
        RuleFor(project => project.ProjectDescription).NotEmpty().WithMessage("Project description name can't be empty");
        RuleFor(project => project.SoftCap).GreaterThan(0).WithMessage("Soft cap can't be 0");
        RuleFor(project => project.HardCap).GreaterThan(0).WithMessage("Hard cap can't be 0");
        RuleFor(project => project.Duration).GreaterThan(0).WithMessage("Duration can't be 0");
        RuleFor(project => project.TokenPrice).GreaterThan(0).WithMessage("Token price can't be 0");
        RuleFor(project => project.TokenFounder[1]).Equal('x').WithMessage("Contract address is incorrect!");
        RuleFor(project => project.TokenFounder).Length(42).WithMessage("Contract address is incorrect!");
        RuleFor(project => project.HardCap).GreaterThan(project => project.SoftCap).WithMessage("Hard cap can't be lower soft cap");
    }
}
