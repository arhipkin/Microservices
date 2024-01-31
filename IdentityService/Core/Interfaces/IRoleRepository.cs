using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<AppRole>> GetAllRolesAsync(CancellationToken cancellationToken = default);
        Task<IdentityResult> CreateRoleAsync(AppRole appUserRole);
        Task<IdentityResult> DeleteRoleAsync(string role);
    }
}
