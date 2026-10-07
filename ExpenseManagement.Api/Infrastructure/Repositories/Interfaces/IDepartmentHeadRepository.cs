using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IDepartmentHeadRepository
{
    public Task<DepartmentHead> CreateDepartmentHeadAsync(DepartmentHead departmentHead);
    public Task<DepartmentHead?> ExistsByEmployeeIdAsync(int employeeId);
    public Task<DepartmentHead?> GetByDepartmentIdAsync(int departmentId);
    public Task<DepartmentHead> UpdateDepartmentHeadAsync(DepartmentHead departmentHead);
}