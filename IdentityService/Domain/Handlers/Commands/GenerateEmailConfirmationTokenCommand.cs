using Core.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Command
{
    public class GenerateEmailConfirmationTokenCommand : IRequest<string>
    {
        public AppUser User { get; set; }
    }
}
