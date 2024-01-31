using Core.Interfaces;
using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Infrastructure.IdentityRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _userManager
                .Users
                .Include(x => x.UserDetails)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<AppUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _userManager
                .Users
                .Include(x => x.UserDetails)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AppUser?> GetUserByNameAsync(string name)
        {
            return await _userManager
                .Users
                .Include(x => x.UserDetails)
                .Where(x => x.UserName == name)
                .FirstOrDefaultAsync();
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager
                .Users
                .Include(x => x.UserDetails)
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> DeleteUserAsync(AppUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckUserPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
    }
}
