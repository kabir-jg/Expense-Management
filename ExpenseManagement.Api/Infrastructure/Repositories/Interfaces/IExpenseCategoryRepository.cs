using ExpenseManagement.Api.Application.Features.ExpenseCategories.Commands;
using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IExpenseCategoryRepository
{
    public Task<ExpenseCategory> CreateExpenseCategoryAsync(ExpenseCategory command);
    public Task<ExpenseCategory?> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken);
}