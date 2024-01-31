﻿using Core.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Commands
{
    public class CreateRoleCommand : IRequest<IdentityResult>
    {
        public AppRole AppUserRole { get; set; }
    }
}
