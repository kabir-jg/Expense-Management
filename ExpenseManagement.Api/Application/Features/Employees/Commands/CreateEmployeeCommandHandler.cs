using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Employee.Commands;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;


public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDTO>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateEmployeeCommandHandler(
        IMapper mapper,
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IUnitOfWork unitOfWork
        )
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        
    }
    
    public async Task<EmployeeDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Check if an employee with the same email already exists
        var emailExists = await _employeeRepository.ExistsByEmailAsync(request.Email);
        if (emailExists)
        {
            throw new ConflictException($"An employee with the email '{request.Email}' already exists.");
        }
        
        // Check if an employee with the same phone number already exists
        var phoneNumberExists = await _employeeRepository.ExistsByPhoneNumberAsync(request.PhoneNumber);
        if (phoneNumberExists)
        {
            throw new ConflictException($"An employee with the phone number '{request.PhoneNumber}' already exists.");
        }
        
        // Begin a new transaction
       _unitOfWork.BeginTransaction();
       try
       {
           var employee = _mapper.Map<Employee>(request);
           var createEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
      
           var user = new User
           {
               EmployeeId = createEmployee.Id,
               Email = request.Email,
               Role = "Employee",
               CreatedAt = DateTime.UtcNow,
               UpdatedAt = DateTime.UtcNow,
           
           };
           var passwordHash = _passwordHasher.HashPassword(user, request.Password);
           user.PasswordHash = passwordHash;
        
           await _userRepository.CreateUserAsync(user);
           _unitOfWork.Commit();
           
           return _mapper.Map<EmployeeDTO>(employee);
       }catch
       {
           _unitOfWork.Rollback();
           throw;
       }
        
    }
}