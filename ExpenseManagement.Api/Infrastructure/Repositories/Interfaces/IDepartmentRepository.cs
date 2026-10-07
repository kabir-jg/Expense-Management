using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;

namespace ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IDepartmentRepository
{
    public Task<Department> CreateDepartmentAsync(Department department);
    public Task<Department> UpdateDepartmentAsync(Department department);
    public Task<GetDepartmentDTO?> GetDepartmentByIdAsync(int departmentId);
    public Task<List<GetDepartmentDTO>> GetDepartmentsAsync();
}