using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Expenses.Commands;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDTO>
{
    private readonly IMapper _mapper;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILineItemRepository _lineItemRepository;
    
    public CreateExpenseCommandHandler(IMapper mapper,
        IExpenseRepository expenseRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork,
        ILineItemRepository lineItemRepository)
    {
        _mapper = mapper;
        _expenseRepository = expenseRepository;
        _expenseCategoryRepository = expenseCategoryRepository;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
        _lineItemRepository = lineItemRepository;
    }
    
    public async Task<ExpenseDTO> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        // Check if the expense category exists
       var expenseCategory =
           await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(request.ExpenseCategoryId, cancellationToken);
       if (expenseCategory == null)
       {
              throw new NotFoundException($"Expense category with ID {request.ExpenseCategoryId} does not exist.");
       }
        // Check if the employee exists
         var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
        if (employee == null)
        {
            throw new NotFoundException($"Employee with ID {request.EmployeeId} does not exist.");
        }
        // Check if the department exists
        var department = await _departmentRepository.GetDepartmentByIdAsync(request.DepartmentId);
        if (department == null)
        {
            throw new NotFoundException($"Department with ID {request.DepartmentId} does not exist.");
        }

        try
        {
            _unitOfWork.BeginTransaction();
            var expense = _mapper.Map<Expense>(request);
            var createdExpense = await _expenseRepository.CreateExpenseAsync(expense);

            var createdLineItems = new List<LineItem>();
            foreach (var lineItemCommand in request.LineItems)
            {
                var lineItem = _mapper.Map<LineItem>(lineItemCommand);
                lineItem.ExpenseId = createdExpense.Id;
                lineItem.CreatedAt = DateTime.UtcNow;
                createdLineItems.Add(await _lineItemRepository.CreateLineItemAsync(lineItem));
            }

            _unitOfWork.Commit();
            var response = _mapper.Map<ExpenseDTO>(createdExpense);
            response.Employee = _mapper.Map<EmployeeBasicInfoDTO>(employee);
            response.Department = _mapper.Map<DepartmentDTO>(department);
            response.ExpenseCategory = _mapper.Map<ExpenseCategoryDTO>(expenseCategory);
            response.LineItems = _mapper.Map<LineItemDTO[]>(createdLineItems);
            return response;
    
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}