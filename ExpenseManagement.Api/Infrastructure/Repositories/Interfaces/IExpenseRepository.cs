using ExpenseManagement.Api.Application.Features.Expenses.Queries;
using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IExpenseRepository
{
    public Task<Expense> CreateExpenseAsync(Expense command);
    public Task<ExpenseDetailRow[]> GetExpensesAsync();
}