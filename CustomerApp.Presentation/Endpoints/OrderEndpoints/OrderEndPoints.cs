using Carter;
using CustomerApp.Application.Authentication;
using CustomerApp.Application.CustomerFeatures.Queries.GetOrders;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Enums;
using CustomerApp.Domain.Results.Customer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace CustomerApp.Presentation.Endpoints.UserEndpoints;

public class OrderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders")
            .WithDisplayName("Manage Orders");

        group.MapGet(string.Empty, GetOrders)
            .WithName(nameof(GetOrders))
            .WithDescription("Get all orders")
            .RequireRateLimiting("token");
    }

    [HasPermission(Permissions.GetOrders)]
    public static async Task<Results<Ok<PagedList<GetOrderResult>>, BadRequest>> GetOrders(string? searchText,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersQuery(searchText, sortColumn, sortOrder, page, pageSize);

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
}
