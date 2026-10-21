using NguyenHuuDung_0299.Models.Academic;

namespace NguyenHuuDung_0299.Models.Users
{
    public class Student
    {
        public Guid UserId { get; set; }
        public required string StudentCode { get; set; }
        public required string Faculty { get; set; }
        public required string Major { get; set; }
        public int EnrollmentYear { get; set; }
        public string? AvatarUrl { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}
