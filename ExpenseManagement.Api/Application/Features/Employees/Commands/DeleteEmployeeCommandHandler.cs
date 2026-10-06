using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.Features.Employee.Commands;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Employees.Commands;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
        if(employee == null)
        {
            throw new NotFoundException($"Employee with ID {request.EmployeeId} not found.");
        }
        
        try
        {
            // Begin a new transaction
            _unitOfWork.BeginTransaction();
            
            // Delete the associated user first
            var isUserDeleted = await _userRepository.DeleteUserByEmployeeIdAsync(employee.Id);

            // Delete the employee
            var isEmployeeDeleted = await _employeeRepository.DeleteEmployeeAsync(request.EmployeeId);
            
            
            if ( !isUserDeleted || !isEmployeeDeleted)
            {
                throw new Exception($"Failed to delete employee or associated user with ID {request.EmployeeId}.");
            }

            // Commit the transaction
            _unitOfWork.Commit();

            return true;
        }
        catch
        {
            // Rollback the transaction in case of an error
            _unitOfWork.Rollback();
            throw;
        }
    }
}