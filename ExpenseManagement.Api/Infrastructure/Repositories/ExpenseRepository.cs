using System.Data;
using System.Text.Json;
using Dapper;
using ExpenseManagement.Api.Application.Features.Expenses.Queries;
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
    
    public async Task<ExpenseDetailRow[]> GetExpensesAsync()
    {
        var sql = """
                    SELECT
                      e.Id,
                      e.Title,
                      e.Description,
                      e.InvoiceAmount,
                      e.SubmittedAt,
                      e.UpdatedAt,
                      e.Status,

                      em.Id AS EmployeeId,
                      em.FirstName,
                      em.LastName,
                      em.Email,
                      em.PhoneNumber,

                      ec.Id AS ExpenseCategoryId,
                      ec.Name AS ExpenseCategoryName,
                      ec.IsActive AS ExpenseCategoryIsActive,

                      de.Id AS DepartmentId,
                      de.Name AS DepartmentName,

                      li.Id AS LineItemId,
                      li.Title AS LineItemTitle,
                      li.Description AS LineItemDescription,
                      li.Amount AS LineItemAmount,
                      li.CreatedAt AS LineItemCreatedAt

                  FROM Expenses e
                  JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
                  JOIN Departments de ON e.DepartmentId = de.Id
                  JOIN Employees em ON e.EmployeeId = em.Id
                  LEFT JOIN LineItems li ON e.Id = li.ExpenseId
                  """;
        var expenses = await _dbConnection.QueryAsync<ExpenseDetailRow>(sql, transaction: _unitOfWork.Transaction);
        return expenses.ToArray();
    }
}