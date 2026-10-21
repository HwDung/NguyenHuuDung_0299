using Microsoft.AspNetCore.Identity;

namespace NguyenHuuDung_0299.Models.Users
{
    public class User : IdentityUser<Guid>
    {
        public required string FullName { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAtUtc { get; set; }
        public Student? Student { get; set; }
        public Lecturer? Lecturer { get; set; }
    }
}
