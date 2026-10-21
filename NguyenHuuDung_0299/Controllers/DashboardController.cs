using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenHuuDung_0299.Data;

namespace NguyenHuuDung_0299.Controllers;

[ApiController, Authorize, Route("api/dashboard")]
public class DashboardController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (User.IsInRole("Admin"))
            return Ok(new { role = "Admin", users = await db.Users.CountAsync(ct), classes = await db.CourseClasses.CountAsync(ct), attendance = await db.AttendanceRecords.CountAsync(ct) });
        if (User.IsInRole("Lecturer"))
            return Ok(new { role = "Lecturer", classes = await db.CourseClasses.CountAsync(x => x.LecturerId == userId, ct), sessions = await db.ClassSessions.CountAsync(x => x.CourseClass.LecturerId == userId, ct) });

        var student = await db.Students.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId, ct);
        if (student is null) return NotFound("Hồ sơ sinh viên chưa được tạo.");
        return Ok(new
        {
            role = "Student",
            student = new { student.StudentCode, student.Faculty, student.Major },
            enrolledClasses = await db.Enrollments.CountAsync(x => x.StudentId == userId && x.Status == Models.Academic.EnrollmentStatus.Active, ct),
            presentCount = await db.AttendanceRecords.CountAsync(x => x.StudentId == userId && (x.Status == Models.Academic.AttendanceStatus.Present || x.Status == Models.Academic.AttendanceStatus.Late), ct)
        });
    }
}
