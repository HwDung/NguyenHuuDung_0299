using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Models.Academic
{
    public class CourseClass
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public int SubjectId { get; set; }
        public Guid LecturerId { get; set; }
        public required string Semester { get; set; }
        public int AcademicYear { get; set; }
        public int Capacity { get; set; }
        public Subject Subject { get; set; } = null!;
        public Lecturer Lecturer { get; set; } = null!;
        public ICollection<ClassSession> Sessions { get; set; } = new List<ClassSession>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
