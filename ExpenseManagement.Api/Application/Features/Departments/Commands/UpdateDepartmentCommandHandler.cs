using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentDTO>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDepartmentHeadRepository _departmentHeadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;
    

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, 
        IDepartmentHeadRepository departmentHeadRepository,
        IUnitOfWork unitOfWork, 
        IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentHeadRepository = departmentHeadRepository;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }
    
    public async Task<UpdateDepartmentDTO> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // Check if the department exists
        var isDepartmentExists = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
        if (isDepartmentExists == null)
        {
            throw new NotFoundException($"Department with Id {request.Id} not found.");
        }
        
        // Check if the Employee Exists
        var isEmployeeExists = await _employeeRepository.GetEmployeeByIdAsync(request.DepartmentHeadEmployeeId);
        if (isEmployeeExists == null)
        {
            throw new NotFoundException($"Department head with Employee Id {request.DepartmentHeadEmployeeId} not found.");
        }
        
        // Check if the Employee is already a department head of another department
        // Check if the employee is already head of another department
        var existingDepartmentHead =
            await _departmentHeadRepository.ExistsByEmployeeIdAsync(
                request.DepartmentHeadEmployeeId);

        if (existingDepartmentHead != null &&
            existingDepartmentHead.DepartmentId != request.Id)
        {
            throw new ConflictException(
                $"Employee with Id {request.DepartmentHeadEmployeeId} " +
                "is already a department head of another department.");
        }
        
        try
        {
           _unitOfWork.BeginTransaction();
           
           // Update the department
           var updatedDepartment = await _departmentRepository.UpdateDepartmentAsync(new Department
           {
               Id = request.Id,
               Name = request.Name
           });
           
           // Update the department head
           var currentDepartmentHead =
               await _departmentHeadRepository.GetByDepartmentIdAsync(request.Id);

           DepartmentHead updatedDepartmentHead;

           if (currentDepartmentHead == null)
           {
               updatedDepartmentHead = await _departmentHeadRepository.CreateDepartmentHeadAsync(new DepartmentHead
               {
                   DepartmentId = request.Id,
                   EmployeeId = request.DepartmentHeadEmployeeId
               });
           }
           else
           {
               updatedDepartmentHead = await _departmentHeadRepository.UpdateDepartmentHeadAsync(new DepartmentHead
               {
                   Id = currentDepartmentHead.Id,
                   DepartmentId = request.Id,
                   EmployeeId = request.DepartmentHeadEmployeeId
               });
           }
           _unitOfWork.Commit();
              
              var result = new UpdateDepartmentDTO
              {
                  Id = updatedDepartment.Id,
                  Name = updatedDepartment.Name,
                  DepartmentHeadEmployeeId = updatedDepartmentHead.EmployeeId,
                  DepartmentHeadEmployeeName = isEmployeeExists.FirstName + " " + isEmployeeExists.LastName,
                  DepartmentHeadEmployeeEmail = isEmployeeExists.Email,
                  DepartmentHeadEmployeePhone = isEmployeeExists.PhoneNumber
              };
              
              return result;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}