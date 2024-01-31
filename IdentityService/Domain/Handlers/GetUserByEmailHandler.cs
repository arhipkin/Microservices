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
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, AppUser?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByNameAsync(request.Email);
        }
    }
}
