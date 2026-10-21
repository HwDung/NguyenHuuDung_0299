using System.ComponentModel.DataAnnotations;

namespace NguyenHuuDung_0299.Models.Auth;

public sealed class CreateStudentRequest
{
    [Required, StringLength(50, MinimumLength = 3)] public required string Username { get; set; }
    [Required, EmailAddress] public required string Email { get; set; }
    [Required, StringLength(150)] public required string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    [MinLength(8)] public string? Password { get; set; }
    public string? Faculty { get; set; }
    public string? Major { get; set; }
    public int? EnrollmentYear { get; set; }
}

public sealed class LoginRequest
{
    [Required] public required string Username { get; set; }
    [Required] public required string Password { get; set; }
}

public sealed class VerifyLoginRequest
{
    [Required] public required string Username { get; set; }
    [Required, StringLength(6, MinimumLength = 6)] public required string Code { get; set; }
}
