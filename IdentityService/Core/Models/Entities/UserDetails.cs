namespace Core.Models.Entities
{
    public class UserDetails
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Description { get; set; }
        public AppUser User { get; set; }
    }
}
