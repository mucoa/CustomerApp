using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Abstractions.User;
using FluentResults;

namespace CustomerApp.Application.UserFeatures.Commands.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository) :
    ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
        => await _userRepository.Create(request.Email, request.Password, cancellationToken)
            .ConfigureAwait(false);
}
