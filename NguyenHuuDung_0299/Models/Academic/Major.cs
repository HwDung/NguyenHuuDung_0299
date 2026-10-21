using System.Text.Json.Serialization;

namespace NguyenHuuDung_0299.Models.Academic
{
    public class Major
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public int FacultyId { get; set; }
        [JsonIgnore]
        public Faculty? Faculty { get; set; }
        public string? Description { get; set; }
    }
}
