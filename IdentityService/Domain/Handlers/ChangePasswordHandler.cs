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
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, IdentityResult>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ChangePasswordAsync(request.User, request.CurrentPassword, request.NewPassword);
        }
    }
}
