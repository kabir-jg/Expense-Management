using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Domain.Enums;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    
    public ExpenseRepository(IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }   
   
    
    public async Task<Expense> CreateExpenseAsync(Expense command)
    {
        command.Status = ExpenseStatusEnum.Pending;
        command.SubmittedAt = DateTime.UtcNow;
        
        var sql = """
                  insert into Expenses
                  (
                   Title,
                   Description,
                   InvoiceAmount,
                   Status,
                   SubmittedAt,
                   EmployeeId,
                   ExpenseCategoryId,
                   DepartmentId
                  )
                  output inserted.Id
                    values
                    (
                        @Title,
                        @Description,
                        @InvoiceAmount,
                        @Status,
                        @SubmittedAt,
                        @EmployeeId,
                        @ExpenseCategoryId,
                        @DepartmentId
                    )
                  """;
        var expenseId = await _dbConnection.QuerySingleAsync<int>(sql, command, _unitOfWork.Transaction);
        command.Id = expenseId;
        return command;
    }
}