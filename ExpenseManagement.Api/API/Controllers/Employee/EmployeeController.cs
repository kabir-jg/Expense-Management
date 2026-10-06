using ExpenseManagement.Api.Application.Common.Models;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Employee.Commands;
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
    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        var response = ApiResponse<object>.SuccessResponse("Employee created successfully", result);    
        return CreatedAtAction(nameof(CreateEmployee), response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await _mediator.Send(new GetEmployeeQuery());
        var response = ApiResponse<EmployeeDTO[]>.SuccessResponse("Employee retrieved successfully", result);
        return Ok(response);
    }
}