using Core.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Command
{
    public class ConfirmEmailCommand : IRequest<IdentityResult>
    {
        public AppUser User { get; set; }
        public string Token { get; set; }
    }
}
