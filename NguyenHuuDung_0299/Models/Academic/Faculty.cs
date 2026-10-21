namespace NguyenHuuDung_0299.Models.Academic
{
    public class Faculty
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Major> Majors { get; set; } = new List<Major>();
    }
}
