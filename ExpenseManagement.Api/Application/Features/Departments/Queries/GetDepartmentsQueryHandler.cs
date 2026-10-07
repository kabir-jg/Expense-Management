using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Queries;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<GetDepartmentDTO>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    
    public GetDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GetDepartmentDTO>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetDepartmentsAsync();
        return _mapper.Map<List<GetDepartmentDTO>>(departments);
    }
}