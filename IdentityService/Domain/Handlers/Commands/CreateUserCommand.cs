using Core.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Handlers.Command
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public AppUser User { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
