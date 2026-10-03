using System.Data;
using Dapper;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IDbConnection _dbConnection;
    
    public AuthRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<bool> EmailExistAsync(string email)
    {
        var sql = """
                  select count(1) from Employees where Email = @Email
                  """;
        var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email });
        return count > 0;
    }
    
    
}