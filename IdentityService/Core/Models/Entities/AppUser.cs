using Microsoft.AspNetCore.Identity;

namespace Core.Models.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        public UserDetails? UserDetails { get; set; }
    }
}
