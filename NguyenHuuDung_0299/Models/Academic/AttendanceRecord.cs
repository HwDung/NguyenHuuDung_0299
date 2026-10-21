using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Models.Academic
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int ClassSessionId { get; set; }
        public Guid StudentId { get; set; }
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
        public DateTime? CheckedInAtUtc { get; set; }
        public string? Note { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? CapturedFaceUrl { get; set; }
        public bool IsFaceVerified { get; set; }
        public string? DeviceInfo { get; set; }
        public ClassSession ClassSession { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }

    public enum AttendanceStatus
    {
        Present = 0, // Có mặt
        Absent = 1,  // Vắng mặt
        Late = 2,    // Đi muộn
        Excused = 3  // Có phép
    }
}
