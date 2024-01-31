using Core.Interfaces;
using Core.Models.Entities;
using Domain.Handlers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<AppRole>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<AppRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAllRolesAsync(cancellationToken);
        }
    }
}
