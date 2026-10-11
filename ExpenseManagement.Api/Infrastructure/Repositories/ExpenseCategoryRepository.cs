using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly IDbConnection _dbConnection;

    public ExpenseCategoryRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ExpenseCategory> CreateExpenseCategoryAsync(ExpenseCategory command)
    {
        var sql = """
                  insert into ExpenseCategories
                  (
                   Name,
                   IsActive
                  )
                    output inserted.Id
                    values
                    (
                        @Name,
                        @IsActive = 1
                    )
                  """;
        var expenseId = await _dbConnection.QuerySingleAsync<int>(sql, command);
        command.Id = expenseId;
        command.IsActive = true;
            
        return command;
    }
    
    public async Task<ExpenseCategory?> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken) 
    {
        var sql = "select * from ExpenseCategories where Id = @Id";
        var expenseCategory = await _dbConnection.QuerySingleOrDefaultAsync<ExpenseCategory?>(sql, new { Id = id });
        return expenseCategory;
    }
}