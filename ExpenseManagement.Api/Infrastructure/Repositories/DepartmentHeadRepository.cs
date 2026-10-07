using System.Data;
using Dapper;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;

namespace ExpenseManagement.Api.Infrastructure.Repositories;

public class DepartmentHeadRepository : IDepartmentHeadRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IUnitOfWork _unitOfWork;
    
    public DepartmentHeadRepository(IDbConnection dbConnection, IUnitOfWork unitOfWork)
    {
        _dbConnection = dbConnection;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DepartmentHead> CreateDepartmentHeadAsync(DepartmentHead departmentHead)
    {
        var sql = """
                insert into 
                DepartmentHeads
                (
                 DepartmentId,
                 EmployeeId
                 )
                output inserted.Id
                values (
                        @DepartmentId,
                        @EmployeeId
                        )
                """;
        var id = await _dbConnection.QuerySingleAsync<int>(sql, departmentHead, transaction: _unitOfWork.Transaction);
        departmentHead.Id = id;
        return departmentHead;
    }
    
    public async Task<DepartmentHead?> ExistsByEmployeeIdAsync(int employeeId)
    {
        var sql = "SELECT * FROM DepartmentHeads WHERE EmployeeId = @EmployeeId";
        var departmentHead = await _dbConnection.QuerySingleOrDefaultAsync<DepartmentHead>(sql,
            new { EmployeeId = employeeId }, transaction: _unitOfWork.Transaction);
        return departmentHead;
    }
    
    public async Task<DepartmentHead?> GetByDepartmentIdAsync(int departmentId)
    {
        var sql = "SELECT * FROM DepartmentHeads WHERE DepartmentId = @DepartmentId";
        var departmentHead = await _dbConnection.QuerySingleOrDefaultAsync<DepartmentHead>(sql,
            new { DepartmentId = departmentId }, transaction: _unitOfWork.Transaction);
        return departmentHead;
    }
    
    public async Task<DepartmentHead> UpdateDepartmentHeadAsync(DepartmentHead departmentHead)
    {
        var sql = """
                update DepartmentHeads
                set EmployeeId = @EmployeeId
                where DepartmentId = @DepartmentId
                """;
        await _dbConnection.ExecuteAsync(sql, departmentHead, transaction: _unitOfWork.Transaction);
        return departmentHead;
    }
}