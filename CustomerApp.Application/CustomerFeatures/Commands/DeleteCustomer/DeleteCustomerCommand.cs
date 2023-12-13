using CustomerApp.Application.Abstractions.Messaging;

namespace CustomerApp.Application.CustomerFeatures.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(Guid Id) : ICommand;
