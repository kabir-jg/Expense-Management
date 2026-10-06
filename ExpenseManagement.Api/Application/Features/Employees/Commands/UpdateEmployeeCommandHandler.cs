using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employees.Commands;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDTO>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<EmployeeDTO> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if (existingEmployee == null)
        {
            throw new NotFoundException($"Employee with ID {request.Id} not found.");
        }
        
        // Check if the phone number is being updated and if it already exists
        if (!string.Equals(existingEmployee.PhoneNumber, request.PhoneNumber, StringComparison.OrdinalIgnoreCase))
        {
            var phoneNumberExists = await _employeeRepository.ExistsByPhoneNumberAsync(request.PhoneNumber);
            if (phoneNumberExists)
            {
                throw new ConflictException($"An employee with the phone number '{request.PhoneNumber}' already exists.");
            }
        }

        // Begin a new transaction
        _unitOfWork.BeginTransaction();
        try
        {
            var updatedEmployee = _mapper.Map(request, existingEmployee);
            updatedEmployee.UpdatedAt = DateTime.UtcNow;
            var result = await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);
            _unitOfWork.Commit();
            return _mapper.Map<EmployeeDTO>(result);
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}