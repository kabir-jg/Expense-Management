namespace ExpenseManagement.Api.Domain.Entities;

public class ExpenseCategory
{
 public int Id { get; set; }
 public string Name { get; set; }
 public string? Description { get; set; }
 public bool IsActive { get; set; }
}