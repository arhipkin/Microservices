using Core.Interfaces;
using Domain.Handlers.Command;
using Domain.Handlers.Commands;
using Infrastructure.IdentityRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, IdentityResult>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.CreateRoleAsync(request.AppUserRole);
        }
    }
}
