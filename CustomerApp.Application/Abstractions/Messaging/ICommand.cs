using FluentResults;
using MediatR;

namespace CustomerApp.Application.Abstractions.Messaging;

internal interface ICommand : IRequest<Result>
{
}

internal interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
