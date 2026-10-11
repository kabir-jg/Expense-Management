using ExpenseManagement.Api.Application.Common.Models;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Expenses.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.API.Controllers.Expense;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize (Policy = "CanCreateExpense")]
    public async Task<IActionResult> CreateExpense(CreateExpenseCommand command, CancellationToken token)
    {
        var result = await _mediator.Send(command, token);
        return StatusCode(StatusCodes.Status201Created, ApiResponse<ExpenseDTO>.SuccessResponse(
            "Expense created successfully.", 
            result));
    }
}