using System.Data;
using Dapper;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IDbConnection _dbConnection;
    
    public AuthRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var sql = """
                  select * from Users where Email = @Email
                  """;
        var user = await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        return user;
    }
    
    
}