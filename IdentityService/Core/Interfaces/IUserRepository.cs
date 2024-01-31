using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<AppUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<AppUser?> GetUserByNameAsync(string name);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<IdentityResult> DeleteUserAsync(AppUser user);
        Task<bool> CheckUserPasswordAsync(AppUser user, string password);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<IdentityResult> AddUserToRolesAsync(AppUser user, IEnumerable<string> roles);
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
    }
}
