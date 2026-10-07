using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDTO>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public CreateDepartmentHandler
    (
        IDepartmentRepository departmentRepository,
        IMapper mapper
    )
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentDTO> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.CreateDepartmentAsync(new Department
        {
            Name = request.Name
        });
        return _mapper.Map<DepartmentDTO>(department);
    }
}