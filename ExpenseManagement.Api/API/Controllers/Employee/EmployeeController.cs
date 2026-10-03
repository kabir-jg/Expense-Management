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
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new
                {
                    message = "An error occurred while creating the employee.",
                    error = ex.Message
                });
        }
    }
}