using ExpenseManagement.Api.Application.DTOs;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task<bool> EmailExistAsync(string email);
}
