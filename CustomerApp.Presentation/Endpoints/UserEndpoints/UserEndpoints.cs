using Carter;
using CustomerApp.Application.Authentication;
using CustomerApp.Application.UserFeatures.Commands.CreateUser;
using CustomerApp.Application.UserFeatures.Commands.LoginWithCredentials;
using CustomerApp.Domain.Entities;
using CustomerApp.Domain.Enums;
using CustomerApp.Domain.Helpers;
using CustomerApp.Presentation.Requests.UserEndpointRequests;
using CustomerApp.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Error = FluentResults.Error;

namespace CustomerApp.Presentation.Endpoints.UserEndpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user")
            .WithDisplayName("Manage User");

        group.MapPost("login", Login)
            .WithName(nameof(Login))
            .WithDescription("Login with credentials")
            .RequireRateLimiting("sliding");

        group.MapPost("create", Create)
            .WithName(nameof(Create))
            .WithDescription("Create user")
            .RequireRateLimiting("sliding");
            
    }

    public static async Task<Results<Ok<string>, NotFound<CustomResult>, BadRequest>> Login(
        [FromBody] LoginRequest request,
        ISender sender, 
        CancellationToken cancellationToken)
    {
        var command = new LoginWithCredentialsCommand(request.Email, request.Password);

        if (sender is null)
        {
            return TypedResults.BadRequest();
        }

        var result = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        if (result.IsFailed)
        {
            var errorMessage = string.Empty;
            if (result.Reasons.Exists(x => x.Message == ErrorTypes.ValidationError))
            {
                var error = result.Reasons
                    .Find(x => x.Message == ErrorTypes.ValidationError)
                    as Error;
                errorMessage = error?.Reasons.ReasonsToString();
            }
            else
            {
                errorMessage = result.Reasons?.ReasonsToString();
            }

            return TypedResults.NotFound(new CustomResult()
            {
                IsSucceed = false,
                ErrorMessage = errorMessage,
            });
        }

        return TypedResults.Ok(result.Value);
    }

    [HasPermission(Permissions.Admin)]
    public static async Task<Results<Ok<CustomResult>, NotFound<CustomResult>, BadRequest>> Create(
        [FromBody] CreateRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Email, request.Password);

        if (sender is null)
        {
            return TypedResults.BadRequest();
        }

        var result = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        if (result.IsFailed)
        {
            var errorMessage = string.Empty;
            if (result.Reasons.Exists(x => x.Message == ErrorTypes.ValidationError))
            {
                var error = result.Reasons
                    .Find(x => x.Message == ErrorTypes.ValidationError)
                    as Error;
                errorMessage = error?.Reasons.ReasonsToString();
            }
            else
            {
                errorMessage = result.Reasons?.ReasonsToString();
            }

            return TypedResults.NotFound(new CustomResult()
            {
                IsSucceed = false,
                ErrorMessage = errorMessage,
            });
        }

        return TypedResults.Ok(new CustomResult { IsSucceed = true, Result = "User created." });
    }
}
