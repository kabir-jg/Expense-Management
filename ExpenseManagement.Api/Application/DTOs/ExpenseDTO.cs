using ExpenseManagement.Api.Domain.Enums;

namespace ExpenseManagement.Api.Application.DTOs;

public class ExpenseDTO
{
    public int Id { get; set; }
    public required string  Title { get; set; }
    public string? Description { get; set; }
    public decimal InvoiceAmount { get; set; }
    public required EmployeeBasicInfoDTO Employee { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ExpenseStatusEnum Status { get; set; }
    public required ExpenseCategoryDTO ExpenseCategory { get; set; }
    public required DepartmentDTO Department { get; set; }
    public required LineItemDTO[] LineItems { get; set; }
}

public class EmployeeBasicInfoDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}