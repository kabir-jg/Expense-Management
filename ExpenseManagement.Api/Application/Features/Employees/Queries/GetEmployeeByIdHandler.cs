using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MediatR;
using ExpenseManagement.Api.Domain.Entities;
using MapsterMapper;

namespace ExpenseManagement.Api.Application.Features.Employees.Queries;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeDTO?> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0)
        {
            throw new ArgumentException("Invalid employee ID.");
        }
        var employee = await _employeeRepository.GetEmployeeByIdAsync(query.Id);
        
        if (employee == null)
        {
           throw new NotFoundException($"Employee with ID {query.Id} not found.");
        }
        return _mapper.Map<EmployeeDTO>(employee);
    }
}