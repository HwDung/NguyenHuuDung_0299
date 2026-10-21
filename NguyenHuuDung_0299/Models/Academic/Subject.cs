namespace NguyenHuuDung_0299.Models.Academic
{
    public class Subject
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public int Credits { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
    }
}
