namespace ExpenseManagement.Api.Application.DTOs;

public class ExpenseCategoryDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}