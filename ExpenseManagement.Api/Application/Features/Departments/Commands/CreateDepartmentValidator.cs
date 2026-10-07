using FluentValidation;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class CreateDepartmentCommandValidator
    : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Department name is required.");
    }
}