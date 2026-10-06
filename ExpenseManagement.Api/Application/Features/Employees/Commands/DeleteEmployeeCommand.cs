using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employee.Commands;

public class DeleteEmployeeCommand : IRequest<bool>
{
    public int EmployeeId { get; set; }
}