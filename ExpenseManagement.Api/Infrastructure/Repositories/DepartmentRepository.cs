using System.Data;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using Dapper;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Infrastructure.Persistence;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    
    public DepartmentRepository(IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Department> CreateDepartmentAsync(Department department)
    {
       var sql = """
                insert into 
                Departments
                (
                 Name
                 )
                output inserted.Id
                values (
                        @Name
                        )
                """;
        var id = await _dbConnection.QuerySingleAsync<int>(sql, department);
        department.Id = id;
        return department;
    }
    
    public async Task<Department> UpdateDepartmentAsync(Department department)
    {
        var sql = """
                update Departments
                set Name = @Name
                where Id = @Id
                """;
        await _dbConnection.ExecuteAsync(sql, department, transaction: _unitOfWork.Transaction);
        return department;
    }
    
    public async Task<GetDepartmentDTO?> GetDepartmentByIdAsync(int departmentId)
    {
        var sql = """
                           SELECT 
                               d.Id,
                               d.name,
                               e.Id AS DepartmentHeadEmployeeId,
                               e.FirstName + ' ' + e.LastName AS DepartmentHeadEmployeeName,
                               e.Email AS DepartmentHeadEmployeeEmail,
                               e.PhoneNumber AS DepartmentHeadEmployeePhone
                           FROM Departments d
                           LEFT JOIN DepartmentHeads dh ON d.Id = dh.DepartmentId
                           LEFT JOIN Employees e ON dh.EmployeeId = e.Id
                           WHERE d.Id = @Id
                           """;
        var department = await _dbConnection.QuerySingleOrDefaultAsync<GetDepartmentDTO>(sql, new { Id = departmentId },
            transaction: _unitOfWork.Transaction);
        return department;
    }
    
    public async Task<List<GetDepartmentDTO>> GetDepartmentsAsync()
    {
        var sql = """
                  SELECT 
                      d.Id,
                      d.name,
                      e.Id AS DepartmentHeadEmployeeId,
                      e.FirstName + ' ' + e.LastName AS DepartmentHeadEmployeeName,
                      e.Email AS DepartmentHeadEmployeeEmail,
                      e.PhoneNumber AS DepartmentHeadEmployeePhone
                  FROM Departments d
                  LEFT JOIN DepartmentHeads dh ON d.Id = dh.DepartmentId
                  LEFT JOIN Employees e ON dh.EmployeeId = e.Id
                  """;
        var departments = await _dbConnection.QueryAsync<GetDepartmentDTO>(sql);
        return departments.ToList();
    }
    
    public async Task<bool> DeleteDepartmentAsync(int departmentId)
    {
        var sql = """
                  DELETE FROM Departments WHERE Id = @Id
                  """;
        var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = departmentId }, transaction: _unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}