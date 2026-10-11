using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class LineItemRepository : ILineItemRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    
    public LineItemRepository(IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }
    public async Task<LineItem> CreateLineItemAsync(LineItem lineItem)
    {
        var sql = @"
            INSERT INTO LineItems (Title, Description, Amount, CreatedAt, ExpenseId)
            OUTPUT INSERTED.Id
            VALUES (@Title, @Description, @Amount, @CreatedAt, @ExpenseId);
            ";
        
        var lineItemId = await _dbConnection.QuerySingleAsync<int>(sql, lineItem, _unitOfWork.Transaction);
        lineItem.Id = lineItemId;
        return lineItem;
    }
}