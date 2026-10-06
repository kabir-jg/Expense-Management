using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employees.Queries;

public class GetEmployeeByIdQuery : IRequest<EmployeeDTO?>
{
    public int Id { get; set; }
}