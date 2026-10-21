namespace NguyenHuuDung_0299.Models.Auth
{
    public class EmailOtp
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string CodeHash { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? UsedAtUtc { get; set; }
        public int Attempts { get; set; }
    }
}
