using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IAuthRepository
{ 
    Task<User?> GetUserByEmailAsync(string email);
    
}
