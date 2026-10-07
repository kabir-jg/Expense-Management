using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Queries;

public class GetDepartmentByIdQuery : IRequest<GetDepartmentDTO?>
{
    public int DepartmentId { get; set; }
}