using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IEmployeeRepository
{
     Task<Employee> CreateEmployeeAsync(Employee employee);
     Task<bool> ExistsByEmailAsync(string email);
     Task<bool> ExistsByPhoneNumberAsync(string phoneNumber);
}