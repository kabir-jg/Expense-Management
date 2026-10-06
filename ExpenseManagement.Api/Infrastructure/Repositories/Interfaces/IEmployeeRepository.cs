using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IEmployeeRepository
{
     Task<Employee> CreateEmployeeAsync(Employee employee);
     Task<bool> ExistsByEmailAsync(string email);
     Task<bool> ExistsByPhoneNumberAsync(string phoneNumber);
     Task<Employee[]> GetAllEmployeesAsync();
     Task<Employee?> GetEmployeeByIdAsync(int id);
     Task<Employee> UpdateEmployeeAsync(Employee employee);
     Task<bool> DeleteEmployeeAsync(int id);
}