using Core.Interfaces;
using Domain.Handlers.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class AddUserToRolesHandler : IRequestHandler<AddUserToRolesCommand, IdentityResult>
    {
        private readonly IUserRepository _userRepository;

        public AddUserToRolesHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Handle(AddUserToRolesCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userRepository.GetUserByIdAsync(request.UserId);

            return await _userRepository.AddUserToRolesAsync(appUser, request.Roles);
        }
    }
}
