using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface ILineItemRepository
{
    public Task<LineItem> CreateLineItemAsync(LineItem lineItem);
}