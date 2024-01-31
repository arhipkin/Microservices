using Core.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Handlers.Command
{
    public class AddUserToRolesCommand : IRequest<IdentityResult>
    {
        public Guid UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
