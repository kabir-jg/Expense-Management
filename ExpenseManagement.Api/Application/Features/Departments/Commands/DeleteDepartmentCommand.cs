using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class DeleteDepartmentCommand : IRequest<bool>
{
    public int DepartmentId { get; set; }
}