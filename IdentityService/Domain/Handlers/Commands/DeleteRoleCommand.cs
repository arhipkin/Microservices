﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Commands
{
    public class DeleteRoleCommand : IRequest<IdentityResult>
    {
        public string RoleName { get; set; }
    }
}
