using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace Infrastructure.IdentityRepository
{
    public class IdentityDBSeed : BackgroundService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private const string AdminName = "admin";
        private const string RoleName = "admin";

        public IdentityDBSeed(
                UserManager<AppUser> userManager,
                RoleManager<AppRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var user = await _userManager.FindByNameAsync(AdminName);

            if (user != null)
            {
                //await _userManager.DeleteAsync(user);
                return;
            }

            var admin = new AppUser
            {
                UserName = AdminName,
                Email = "admin@admin.adm",
                LockoutEnabled = false
            };

            admin.UserDetails = new UserDetails
            {
                Description = "admin superpower user"
            };

            await _userManager.CreateAsync(admin, "P@ssw0rd");

            if (await _roleManager.FindByNameAsync(RoleName) == null)
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = RoleName,
                    NormalizedName = RoleName
                });
            }

            await _userManager.AddToRoleAsync(admin, RoleName);
        }
    }
}
