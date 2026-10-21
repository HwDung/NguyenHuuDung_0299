using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Models.Academic
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseClassId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime EnrolledAtUtc { get; set; } = DateTime.UtcNow;
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        public CourseClass CourseClass { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }

    public enum EnrollmentStatus
    {
        Active,
        Dropped,
        Completed
    }
}
