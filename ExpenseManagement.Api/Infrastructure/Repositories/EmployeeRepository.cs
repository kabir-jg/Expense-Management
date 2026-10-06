using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    public EmployeeRepository(IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var sql = "SELECT COUNT(1) FROM Employees WHERE Email = @Email";
        var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email }, transaction: _unitOfWork.Transaction);
        return count > 0;
    }

    public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber)
    {
        var sql = "SELECT COUNT(1) FROM Employees WHERE PhoneNumber = @PhoneNumber";
        var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { PhoneNumber = phoneNumber }, transaction: _unitOfWork.Transaction);
        return count > 0;
    }
    
    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
      var sql = """
                insert into 
                Employees
                (
                 FirstName,
                 LastName,
                 Email,
                 PhoneNumber,
                 Address,
                    City,
                    State,
                    ZipCode,
                    Country,
                    DateOfBirth,
                    Salary,
                    Position,
                    DepartmentId,
                    IsActive,
                    CreatedAt,
                    UpdatedAt,
                    JoiningDate
                 )
                output inserted.Id
                values (
                        @FirstName,
                        @LastName,
                        @Email,
                        @PhoneNumber,
                        @Address,
                        @City,
                        @State,
                        @ZipCode,
                        @Country,
                        @DateOfBirth,
                        @Salary,
                        @Position,
                        @DepartmentId,
                        @IsActive,
                        @CreatedAt,
                        @UpdatedAt,
                        @JoiningDate
                );
                """;
      
      var employeeId = await _dbConnection.QuerySingleAsync<int>(
          sql,
          employee,
          transaction: _unitOfWork.Transaction
      );
        employee.Id = employeeId;
        return employee;
    }
    
    public async Task<Employee[]> GetAllEmployeesAsync()
    {
        var sql = """
                  SELECT * FROM Employees
                  """;
        var employees = await _dbConnection.QueryAsync<Employee>(sql);
        return employees.ToArray();
    }
    
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        var sql = """
                  SELECT * FROM Employees WHERE Id = @Id
                  """;
        var employee = await _dbConnection.QuerySingleOrDefaultAsync<Employee>(sql, new { Id = id });
        return employee;
    }
    
    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var sql = """
                  UPDATE Employees
                  SET FirstName = @FirstName,
                      LastName = @LastName,
                      Email = @Email,
                      PhoneNumber = @PhoneNumber,
                      Address = @Address,
                      City = @City,
                      State = @State,
                      ZipCode = @ZipCode,
                      Country = @Country,
                      DateOfBirth = @DateOfBirth,
                      Salary = @Salary,
                      Position = @Position,
                      DepartmentId = @DepartmentId,
                      IsActive = @IsActive,
                      UpdatedAt = @UpdatedAt,
                      JoiningDate = @JoiningDate
                  OUTPUT INSERTED.*
                  WHERE Id = @Id;
                  """;
        var updatedEmployee = await _dbConnection.QuerySingleAsync<Employee>(sql, employee, transaction: _unitOfWork.Transaction);
        return updatedEmployee;
    }
    
    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var sql = """
                  DELETE FROM Employees WHERE Id = @Id
                  """;
        var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction: _unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}