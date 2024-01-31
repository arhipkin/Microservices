using Core.Models.Entities;
using MediatR;

namespace Domain.Handlers.Queries
{
    public class GetUserByEmailQuery : IRequest<AppUser>
    {
        public string Email { get; set; }
    }
}
