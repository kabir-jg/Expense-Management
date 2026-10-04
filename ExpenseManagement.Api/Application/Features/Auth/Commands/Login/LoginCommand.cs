using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Auth.Commands.Login;

public class LoginCommand: IRequest<AuthCredentialDTO>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}