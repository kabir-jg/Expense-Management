using FluentValidation;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class UpdateDepartmentCommandValidator 
    : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DepartmentHeadEmployeeId)
            .GreaterThan(0);
    }
}