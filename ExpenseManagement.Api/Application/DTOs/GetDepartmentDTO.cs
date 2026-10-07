namespace ExpenseManagement.Api.Application.DTOs;

public class GetDepartmentDTO
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required int DepartmentHeadEmployeeId { get; set; }
   public required string DepartmentHeadEmployeeName { get; set; }
   public required string DepartmentHeadEmployeeEmail { get; set; }
   public string? DepartmentHeadEmployeePhone { get; set; }
}