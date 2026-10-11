using FluentValidation;

namespace ExpenseManagement.Api.Application.Features.Expenses.Commands;

public class CreateExpenseCommandValidator
    : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.InvoiceAmount)
            .GreaterThan(0);

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0);

        RuleFor(x => x.LineItems)
            .NotEmpty();

        RuleForEach(x => x.LineItems).ChildRules(lineItem =>
        {
            lineItem.RuleFor(item => item.Title)
                .NotEmpty()
                .MaximumLength(200);

            lineItem.RuleFor(item => item.Amount)
                .GreaterThan(0);
        });

        RuleFor(x => x.InvoiceAmount)
            .Equal(x => x.LineItems.Sum(item => item.Amount))
            .When(x => x.LineItems is { Length: > 0 })
            .WithMessage("Invoice amount must equal the sum of the line item amounts.");
    }
}