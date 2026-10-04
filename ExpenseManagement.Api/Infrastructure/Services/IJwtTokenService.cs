using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}