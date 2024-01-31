using Core.Interfaces;
using Domain.Handlers.Command;
using Domain.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, IdentityResult>
    {
        public readonly IUserRepository _userRepository;

        public DeleteUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
