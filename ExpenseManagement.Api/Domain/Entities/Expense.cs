namespace ExpenseManagement.Api.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal InvoiceAmount { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int EmployeeId { get; set; }
    public string Status { get; set; }
    public int ExpenseCategoryId { get; set; }
    public int DepartmentId { get; set; }
}