using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IEmployeeRepository
{
     Task<Employee> CreateEmployeeAsync(Employee employee);
}