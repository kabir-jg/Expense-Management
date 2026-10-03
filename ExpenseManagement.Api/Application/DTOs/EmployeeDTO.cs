namespace ExpenseManagement.Api.Application.DTOs;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public  string Country { get; set; }
    public  DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string Position { get; set; }
    public int DepartmentId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateOnly JoiningDate { get; set; }
}