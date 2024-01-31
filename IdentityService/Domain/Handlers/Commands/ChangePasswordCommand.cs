using Core.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Handlers.Command
{
    public class ChangePasswordCommand : IRequest<IdentityResult>
    {
        public AppUser User { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
