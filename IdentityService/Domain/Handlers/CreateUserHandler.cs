using Core.Interfaces;
using Domain.Handlers.Command;
using Infrastructure.IdentityRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IdentityAppDBContext _identityAppDBContext;

        public CreateUserHandler(
            IUserRepository userRepository,
            IdentityAppDBContext identityAppDBContext)
        {
            _userRepository = userRepository;
            _identityAppDBContext = identityAppDBContext;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _identityAppDBContext.BeginTransactionAsync(cancellationToken);

            try
            {
                await _userRepository.CreateUserAsync(request.User, request.Password);
                var result = await _userRepository.AddUserToRolesAsync(request.User, request.Roles);
                await _identityAppDBContext.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                await _identityAppDBContext.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
