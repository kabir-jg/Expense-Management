namespace ExpenseManagement.Api.Application.Features.Auth.Commands.Login;

public class LoginCommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}