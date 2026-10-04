using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using ExpenseManagement.Api.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManagement.Api.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthCredentialDTO>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(IAuthRepository authRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository; 
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthCredentialDTO> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetUserByEmailAsync(command.Email);
        if(user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);
        if(verificationResult == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = _jwtTokenService.GenerateToken(user);
        return new AuthCredentialDTO
        {
            Token = token,
        };
    }
}