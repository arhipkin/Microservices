using Core.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Queries
{
    public class GetUserRolesQuery : IRequest<IEnumerable<string>>
    {
    }
}
