using Microsoft.AspNetCore.Identity;

namespace Core.Models.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public UserDetails? UserDetails { get; set; }
    }
}
