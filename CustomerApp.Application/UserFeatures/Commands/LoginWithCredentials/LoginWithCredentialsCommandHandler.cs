using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Abstractions.User;
using FluentResults;

namespace CustomerApp.Application.UserFeatures.Commands.LoginWithCredentials;

internal sealed class LoginWithCredentialsCommandHandler(IUserRepository repository) :
    ICommandHandler<LoginWithCredentialsCommand, string>
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<string>> Handle(
        LoginWithCredentialsCommand request,
        CancellationToken cancellationToken)
    => await _repository.Login(request.Email, request.Password, cancellationToken).ConfigureAwait(false);
}
