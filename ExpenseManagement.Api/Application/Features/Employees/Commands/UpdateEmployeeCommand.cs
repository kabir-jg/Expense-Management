using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employees.Commands;

public class UpdateEmployeeCommand : IRequest<EmployeeDTO>
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public required decimal Salary { get; set; }
    public required string Position { get; set; }
    public required int DepartmentId { get; set; }
}