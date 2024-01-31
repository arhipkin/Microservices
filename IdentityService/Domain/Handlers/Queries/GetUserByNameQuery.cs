using Core.Models.Entities;
using MediatR;

namespace Domain.Handlers.Queries
{
    public class GetUserByNameQuery : IRequest<AppUser>
    {
        public string Name { get; set; }
    }
}
