using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Queries;

public class GetDepartmentsQuery : IRequest<List<GetDepartmentDTO>>
{
    
}