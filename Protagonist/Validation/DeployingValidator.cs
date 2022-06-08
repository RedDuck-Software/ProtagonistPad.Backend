using FluentValidation;
using Protagonist.Models;

namespace Protagonist.Validation;

public class DeployingValidator : AbstractValidator<ProjectModel>
{
    public DeployingValidator()
    {
        RuleFor(project => project.Status).Equal(ProjectStatus.Approved).WithMessage("Project is rejected or already deployed");
    }  
}
