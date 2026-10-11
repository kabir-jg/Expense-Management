using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Expenses.Queries;

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, ExpenseDTO[]>
{
    private readonly IExpenseRepository _expenseRepository;
    
    public GetExpensesQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }
    
    public async Task<ExpenseDTO[]> Handle(
        GetExpensesQuery request,
        CancellationToken cancellationToken)
    {
        var rows = await _expenseRepository.GetExpensesAsync();

        var expenses = rows
            .GroupBy(x => x.Id)
            .Select(group =>
            {
                var e = group.First();

                return new ExpenseDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    InvoiceAmount = e.InvoiceAmount,
                    SubmittedAt = e.SubmittedAt,
                    UpdatedAt = e.UpdatedAt,
                    Status = e.Status,

                    Employee = new EmployeeBasicInfoDTO
                    {
                        Id = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        PhoneNumber = e.PhoneNumber
                    },

                    ExpenseCategory = new ExpenseCategoryDTO
                    {
                        Id = e.ExpenseCategoryId,
                        Name = e.ExpenseCategoryName,
                        IsActive = e.ExpenseCategoryIsActive
                    },

                    Department = new DepartmentDTO
                    {
                        Id = e.DepartmentId,
                        Name = e.DepartmentName
                    },

                    LineItems = group
                        .Where(x => x.LineItemId.HasValue)
                        .Select(x => new LineItemDTO
                        {
                            Id = x.LineItemId!.Value,
                            Title = x.LineItemTitle!,
                            Description = x.LineItemDescription,
                            Amount = x.LineItemAmount!.Value,
                            CreatedAt = x.LineItemCreatedAt!.Value
                        })
                        .ToArray()
                };
            })
            .ToArray();

        return expenses;
    }
}
