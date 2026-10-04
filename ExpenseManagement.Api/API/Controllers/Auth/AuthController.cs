using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.API.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthCredentialDTO>> Login(LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}