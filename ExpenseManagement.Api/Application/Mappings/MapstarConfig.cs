using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Auth.Commands.Login;
using Mapster;

namespace ExpenseManagement.Api.Application.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<LoginCommand, AuthCredentialDTO>.NewConfig();
    }
}