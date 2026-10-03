using ExpenseManagement.Api.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.API.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    [HttpPost("signup")]
    public async Task<AuthCredentialDTO> Signup()
    {
        return null;
    }
}