namespace NguyenHuuDung_0299.Services;

public sealed class JwtOptions
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int ExpirationMinutes { get; set; } = 60;
}

public sealed class SmtpOptions
{
    public required string Host { get; set; }
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public required string FromEmail { get; set; }
    public string FromName { get; set; } = "Student Attendance";
}
