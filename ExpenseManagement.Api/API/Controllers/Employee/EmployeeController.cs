using ExpenseManagement.Api.Application.Common.Models;
using ExpenseManagement.Api.Application.Features.Employee.Commands;
using MediatR;
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
}