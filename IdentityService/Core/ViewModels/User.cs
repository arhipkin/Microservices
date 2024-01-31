namespace Core.ViewModels
{
    public class User
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public UserDetails? UserDetails { get; set; }
    }
}
