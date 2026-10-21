using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Models.Auth
{
    public class LoginOtp
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public required string CodeHash { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? UsedAtUtc { get; set; }
        public int Attempts { get; set; }
        public User User { get; set; } = null!;
    }
}
