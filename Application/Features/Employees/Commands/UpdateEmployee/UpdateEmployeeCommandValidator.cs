using FluentValidation;

namespace Application.Features.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Employee name is required.")
            .MaximumLength(100).WithMessage("Employee name must not exceed 100 characters.");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("DepartmentId must be greater than 0.");
    }
}
