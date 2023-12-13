using FluentResults;
using MediatR;

namespace CustomerApp.Application.Abstractions.Messaging;

internal interface IQuery : IRequest<Result>
{
}

internal interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
