using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Domain.Entities.Customers;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(Guid Id) : IQuery<Customer>;
