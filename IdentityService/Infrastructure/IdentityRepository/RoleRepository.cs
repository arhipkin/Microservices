using Core.Interfaces;
using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleRepository(
            RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<AppRole>> GetAllRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _roleManager.Roles.ToArrayAsync();
        }

        public async Task<IdentityResult> CreateRoleAsync(AppRole appUserRole)
        {
            return await _roleManager.CreateAsync(appUserRole);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string role)
        {
            var appUserRole = await _roleManager.FindByNameAsync(role);

            return await _roleManager.DeleteAsync(appUserRole);
        }
    }
}
