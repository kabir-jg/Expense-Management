using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class CreateDepartmentCommand : IRequest<DepartmentDTO>
{
    public string Name { get; set; }
}