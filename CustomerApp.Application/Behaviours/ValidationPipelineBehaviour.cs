using CustomerApp.Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace CustomerApp.Application.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        var errors = _validators
           .Select(validator => validator.Validate(request))
           .SelectMany(validationResult => validationResult.Errors)
           .Where(validationFailure => validationFailure is not null)
           .Select(x => new Error(x.ErrorMessage))
           .Distinct()
           .ToArray();

        if (errors.Length != 0)
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next().ConfigureAwait(false);
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : ResultBase, new()
    {
        var error = new Error(ErrorTypes.ValidationError);

        error.CausedBy(errors);

        var result = new TResult();
        result.Reasons.Add(error);

        return result;
    }
}
