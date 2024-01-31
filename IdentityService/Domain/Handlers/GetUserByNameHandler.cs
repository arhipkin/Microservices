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
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, AppUser>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByNameAsync(request.Name);
        }
    }
}
