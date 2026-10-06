using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employees.Queries;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDTO[]>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeDTO[]> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        return employees.Select(e => _mapper.Map<EmployeeDTO>(e)).ToArray();
    }
}