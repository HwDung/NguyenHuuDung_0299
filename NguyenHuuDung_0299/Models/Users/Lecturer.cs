using NguyenHuuDung_0299.Models.Academic;

namespace NguyenHuuDung_0299.Models.Users
{
    public class Lecturer
    {
        public Guid UserId { get; set; }
        public required string LecturerCode { get; set; }
        public required string Department { get; set; }
        public User User { get; set; } = null!;
        public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
    }
}
