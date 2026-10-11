namespace ExpenseManagement.Api.Domain.Entities;

public class ExpenseCategory
{
 public int Id { get; set; }
 public required string Name { get; set; }
 public bool IsActive { get; set; }
}