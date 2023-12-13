using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Entities.Customers;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetCustomers;

public sealed record GetCustomersQuery(string? SearchText,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<Customer>>;
