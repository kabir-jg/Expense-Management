using ExpenseManagement.Api.Application.Common.Models;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Employee.Commands;
using ExpenseManagement.Api.Application.Features.Employees.Commands;
using ExpenseManagement.Api.Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.API.Controllers.Employee;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Authorize(Policy = "CanCreateEmployee")]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        var response = ApiResponse<object>.SuccessResponse("Employee created successfully", result);    
        return CreatedAtAction(nameof(CreateEmployee), response);
    }

    [HttpGet]
    [Authorize (Policy = "CanViewEmployee")]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await _mediator.Send(new GetEmployeeQuery());
        var response = ApiResponse<EmployeeDTO[]>.SuccessResponse("Employee retrieved successfully", result);
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    [Authorize (Policy = "CanUpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Employee ID mismatch", 400));
        }

        var result = await _mediator.Send(command);
        var response = ApiResponse<EmployeeDTO>.SuccessResponse("Employee updated successfully", result);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize (Policy = "CanViewEmployee")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery { Id = id });
        var response = ApiResponse<EmployeeDTO?>.SuccessResponse("Employee retrieved successfully", result);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [Authorize (Policy = "CanDeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
       var result = await _mediator.Send(new DeleteEmployeeCommand { EmployeeId = id });
       var response = ApiResponse<object>.SuccessResponse("Employee deleted successfully", result);
       return Ok(response);
    }
}