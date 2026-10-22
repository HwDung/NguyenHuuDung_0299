using System.ComponentModel.DataAnnotations;
using NguyenHuuDung_0299.Models.Academic;

namespace NguyenHuuDung_0299.Models.Academic
{
    public sealed class CreateLecturerRequest
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, StringLength(150)]
        public required string FullName { get; set; }

        public string? PhoneNumber { get; set; }

        [MinLength(8)]
        public string? Password { get; set; }

        [Required]
        public required string Department { get; set; }
    }

    public sealed class UpdateAvatarRequest
    {
        [Required]
        public required string AvatarUrl { get; set; }
    }

    public sealed class AddStudentsToClassRequest
    {
        [Required]
        public List<Guid> StudentIds { get; set; } = new();
    }

    public sealed class OpenAttendanceRequest
    {
        public int? DurationMinutes { get; set; } = 15;
    }

    public sealed class StudentCheckInRequest
    {
        [Required]
        public int SessionId { get; set; }

        public string? ClassCode { get; set; }

        [Required]
        public required string CheckInCode { get; set; }

        public string? CapturedFaceImage { get; set; }

        public bool FaceMatched { get; set; } = true;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? DeviceInfo { get; set; }
    }

    public sealed class UpdateAttendanceStatusRequest
    {
        [Required]
        public AttendanceStatus Status { get; set; }

        public string? Note { get; set; }
    }

    public sealed class UpdateStudentRequest
    {
        [Required, StringLength(150)]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public string? StudentCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Faculty { get; set; }
        public string? Major { get; set; }
        public int? EnrollmentYear { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsActive { get; set; }
    }

    public sealed class UpdateLecturerRequest
    {
        [Required, StringLength(150)]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public required string Department { get; set; }

        public bool? IsActive { get; set; }
    }
}
