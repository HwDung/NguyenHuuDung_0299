namespace NguyenHuuDung_0299.Models.Academic
{
    public class ClassSession
    {
        public int Id { get; set; }
        public int CourseClassId { get; set; }
        public DateTime StartsAtUtc { get; set; }
        public DateTime EndsAtUtc { get; set; }
        public required string Room { get; set; }
        public string? CheckInCode { get; set; }
        public bool IsOpenForAttendance { get; set; }
        public DateTime? AttendanceExpiresAtUtc { get; set; }
        public CourseClass CourseClass { get; set; } = null!;
        public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}
