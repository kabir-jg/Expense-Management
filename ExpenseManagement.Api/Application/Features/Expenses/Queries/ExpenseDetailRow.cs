using ExpenseManagement.Api.Domain.Enums;

namespace ExpenseManagement.Api.Application.Features.Expenses.Queries;

public class ExpenseDetailRow
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal InvoiceAmount { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ExpenseStatusEnum Status { get; set; }

    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public int ExpenseCategoryId { get; set; }
    public string ExpenseCategoryName { get; set; } = string.Empty;
    public bool ExpenseCategoryIsActive { get; set; }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public int? LineItemId { get; set; }
    public string? LineItemTitle { get; set; }
    public string? LineItemDescription { get; set; }
    public decimal? LineItemAmount { get; set; }
    public DateTime? LineItemCreatedAt { get; set; }
}