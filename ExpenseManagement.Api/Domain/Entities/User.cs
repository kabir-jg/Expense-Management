namespace ExpenseManagement.Api.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required string Email {get; set;} = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public required string Role {get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
   
}