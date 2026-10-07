using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class UpdateDepartmentCommand : IRequest<UpdateDepartmentDTO>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int DepartmentHeadEmployeeId { get; set; }
}