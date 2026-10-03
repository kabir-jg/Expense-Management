using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserRepository (IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        var sql = """
                  insert into Users
                  (
                   EmployeeId,
                    Email,
                    PasswordHash,
                    Role,
                    CreatedAt,
                    UpdatedAt
                  )
                  output inserted.Id
                  values (
                          @EmployeeId,
                          @Email,
                          @PasswordHash,
                          @Role,
                          @CreatedAt,
                          @UpdatedAt
                  );
                  """;
        var userId = await _dbConnection.QuerySingleAsync<int>(sql, user, transaction: _unitOfWork.Transaction);
        user.Id = userId;
        return user;
    }
}