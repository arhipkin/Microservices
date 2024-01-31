using Core.Interfaces;
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
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, IdentityResult>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.DeleteRoleAsync(request.RoleName);
        }
    }
}
