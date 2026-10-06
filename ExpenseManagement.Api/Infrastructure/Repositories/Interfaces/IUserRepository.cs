using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<bool> DeleteUserByEmployeeIdAsync(int employeeId);
}