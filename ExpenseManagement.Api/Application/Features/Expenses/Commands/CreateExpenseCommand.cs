using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Expenses.Commands;

public class CreateExpenseCommand : IRequest<ExpenseDTO>
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal InvoiceAmount { get; set; }
    public int EmployeeId { get; set; } // F_K
    public int DepartmentId { get; set; } // F_K
    public int ExpenseCategoryId { get; set; } // F_K
    public required LineItemCommand[] LineItems { get; set; }
    
}