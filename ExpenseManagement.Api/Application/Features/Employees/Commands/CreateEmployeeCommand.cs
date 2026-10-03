using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employee.Commands;


public class CreateEmployeeCommand : IRequest<EmployeeDTO>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public required string Position { get; set; }
    public int DepartmentId { get; set; }
    public DateOnly JoiningDate { get; set; }
    public required string Password { get; set; }
}