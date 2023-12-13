using Carter;
using CustomerApp.Application.Authentication;
using CustomerApp.Application.CustomerFeatures.Commands.SaveCustomer;
using CustomerApp.Application.CustomerFeatures.Queries.GetCustomers;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Entities;
using CustomerApp.Domain.Entities.Customers;
using CustomerApp.Domain.Enums;
using CustomerApp.Presentation.Requests.CustomerEndpointRequests;
using CustomerApp.Presentation.Results;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CustomerApp.Domain.Helpers;
using CustomerApp.Application.CustomerFeatures.Queries.GetCustomerById;
using CustomerApp.Application.CustomerFeatures.Commands.UpdateCustomer;
using CustomerApp.Application.CustomerFeatures.Commands.DeleteCustomer;

namespace CustomerApp.Presentation.Endpoints.CustomerEndPoints;

public class CustomerEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customers")
            .WithDisplayName("Manage Customers");

        group.MapGet(string.Empty, GetCustomers)
            .WithName(nameof(GetCustomers))
            .WithDescription("Get all orders")
            .RequireRateLimiting("token");

        group.MapPost(string.Empty, SaveCustomer)
            .WithName(nameof(SaveCustomer))
            .WithDescription("Save customer")
            .RequireRateLimiting("sliding");

        group.MapGet("{id:Guid}", GetCustomerById)
            .WithName(nameof(GetCustomerById))
            .WithDescription("Get customer by id")
            .RequireRateLimiting("token");

        group.MapPut(string.Empty, UpdateCustomer)
            .WithName(nameof(UpdateCustomer))
            .WithDescription("Update customer")
            .RequireRateLimiting("sliding");
        
        group.MapDelete("{id:Guid}", DeleteCustomer)
            .WithName(nameof(DeleteCustomer))
            .WithDescription("Delete customer")
            .RequireRateLimiting("sliding");
    }

    [HasPermission(Permissions.DeleteCustomer)]
    public static async Task<Results<Ok, BadRequest<CustomResult>>> DeleteCustomer(Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id); 

        if (sender is null)
        {
            return TypedResults.BadRequest(new CustomResult { IsSucceed = false });
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

            return TypedResults.BadRequest(new CustomResult { IsSucceed = false, ErrorMessage = errorMessage });
        }

        return TypedResults.Ok();
    }

    [HasPermission(Permissions.UpdateCustomer)]
    public static async Task<Results<Ok, BadRequest<CustomResult>>> UpdateCustomer(
        [FromBody] UpdateCustomerRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(request.Id, 
            request.Identity,
            request.Name,
            request.PhoneNumber,
            request.EmailAddress,
            request.Address,
            request.Birthdate,
            request.Company,
            request.Orders);

        if (sender is null)
        {
            return TypedResults.BadRequest(new CustomResult { IsSucceed = false });
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

            return TypedResults.BadRequest(new CustomResult { IsSucceed = false, ErrorMessage = errorMessage });
        }

        return TypedResults.Ok();
    }

    [HasPermission(Permissions.UpdateCustomer)]
    public static async Task<Results<Ok<Customer>, BadRequest<CustomResult>>> GetCustomerById(Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);

        if (sender is null)
        {
            return TypedResults.BadRequest(new CustomResult { IsSucceed = false });
        }

        var result = await sender.Send(query, cancellationToken)
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

            return TypedResults.BadRequest(new CustomResult { IsSucceed = false, ErrorMessage = errorMessage });
        }

        return TypedResults.Ok(result.Value);
    }

    [HasPermission(Permissions.Admin)]
    public static async Task<Results<Ok<PagedList<Customer>>, BadRequest>> GetCustomers(string? searchText,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetCustomersQuery(searchText, sortColumn, sortOrder, page, pageSize);

        if (sender is null)
        {
            return TypedResults.BadRequest();
        }

        var result = await sender.Send(query, cancellationToken)
            .ConfigureAwait(false);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(result.Value);
    }

    [HasPermission(Permissions.CreateCustomer)]
    public static async Task<Results<Ok, BadRequest<CustomResult>>> SaveCustomer(
        [FromBody] SaveCustomerRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new SaveCustomerCommand(request.Identity,
            request.Name,
            request.PhoneNumber,
            request.EmailAddress,
            request.Address,
            request.Birthdate,
            request.Company);

        if (sender is null)
        {
            return TypedResults.BadRequest(new CustomResult { IsSucceed = false });
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

            return TypedResults.BadRequest(new CustomResult { IsSucceed = false, ErrorMessage = errorMessage });
        }

        return TypedResults.Ok();
    }


}
