using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NguyenHuuDung_0299.Models.Academic;
using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Data;

public static class DbInitializer
{
    public static async Task SeedAllAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        // 1. Đảm bảo các Roles tồn tại
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new Role { Name = "Admin" });
        if (!await roleManager.RoleExistsAsync("Lecturer"))
            await roleManager.CreateAsync(new Role { Name = "Lecturer" });
        if (!await roleManager.RoleExistsAsync("Student"))
            await roleManager.CreateAsync(new Role { Name = "Student" });

        // 2. Seed tài khoản Admin
        var adminEmail = "huudungn054@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new User
            {
                UserName = "admin",
                Email = adminEmail,
                FullName = "Quản trị viên Hệ thống",
                EmailConfirmed = true,
                IsEmailVerified = true,
                IsActive = true
            };
            var result = await userManager.CreateAsync(adminUser, "Admin@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
        else
        {
            adminUser.EmailConfirmed = true;
            adminUser.IsEmailVerified = true;
            adminUser.IsActive = true;
            await userManager.UpdateAsync(adminUser);

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // 3. Seed Môn học (Subjects)
        var sampleSubjects = new List<Subject>
        {
            new() { Code = "INT101", Name = "Lập trình Cơ bản C/C++", Credits = 3, Description = "Nhập môn lập trình và thuật toán cơ bản", IsActive = true },
            new() { Code = "INT102", Name = "Lập trình hướng đối tượng", Credits = 3, Description = "Lập trình OOP với C# / Java", IsActive = true },
            new() { Code = "INT103", Name = "Cấu trúc dữ liệu và giải thuật", Credits = 3, Description = "Giải thuật, cây, đồ thị, tìm kiếm", IsActive = true },
            new() { Code = "INT104", Name = "Cơ sở dữ liệu", Credits = 3, Description = "Mô hình quan hệ, SQL, chuẩn hóa dữ liệu", IsActive = true },
            new() { Code = "INT105", Name = "Phát triển ứng dụng Web", Credits = 3, Description = "ASP.NET Core Web API, React, RESTful API", IsActive = true },
            new() { Code = "INT106", Name = "Phát triển ứng dụng Di động", Credits = 3, Description = "Flutter / React Native, Mobile app", IsActive = true },
            new() { Code = "INT107", Name = "Trí tuệ nhân tạo", Credits = 3, Description = "Machine Learning, Computer Vision, Deep Learning", IsActive = true },
            new() { Code = "INT108", Name = "An toàn thông tin", Credits = 3, Description = "Bảo mật ứng dụng, mã hóa, an ninh mạng", IsActive = true }
        };

        foreach (var sub in sampleSubjects)
        {
            if (!await db.Subjects.AnyAsync(s => s.Code == sub.Code))
            {
                db.Subjects.Add(sub);
            }
        }
        await db.SaveChangesAsync();

        // 4. Seed Giảng viên (Lecturers)
        var lecturersData = new[]
        {
            (Username: "gv_nguyenvanan", Email: "nguyenvanan@gmail.com", FullName: "ThS. Nguyễn Văn An", Code: "GV000001", Dept: "Công nghệ phần mềm"),
            (Username: "gv_tranthibinh", Email: "tranthibinh@gmail.com", FullName: "TS. Trần Thị Bình", Code: "GV000002", Dept: "Hệ thống thông tin"),
            (Username: "gv_lehoangcuong", Email: "lehoangcuong@gmail.com", FullName: "ThS. Lê Hoàng Cường", Code: "GV000003", Dept: "Mạng và An ninh mạng")
        };

        var lecturerEntities = new List<Lecturer>();
        foreach (var l in lecturersData)
        {
            var user = await userManager.FindByNameAsync(l.Username);
            if (user is null)
            {
                user = new User
                {
                    UserName = l.Username,
                    Email = l.Email,
                    FullName = l.FullName,
                    PhoneNumber = "0901234567",
                    EmailConfirmed = true,
                    IsEmailVerified = true,
                    IsActive = true
                };
                var createRes = await userManager.CreateAsync(user, "Lecturer@12345");
                if (createRes.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Lecturer");
                    var lecturer = new Lecturer
                    {
                        UserId = user.Id,
                        User = user,
                        LecturerCode = l.Code,
                        Department = l.Dept
                    };
                    db.Lecturers.Add(lecturer);
                    lecturerEntities.Add(lecturer);
                }
            }
            else
            {
                var lec = await db.Lecturers.FindAsync(user.Id);
                if (lec != null) lecturerEntities.Add(lec);
            }
        }
        await db.SaveChangesAsync();

        // 5. Seed Sinh viên (Students)
        var studentsData = new[]
        {
            (Username: "huudung_sv", Email: "huudung.student@gmail.com", FullName: "Nguyễn Hữu Dũng", Code: "SV000299", Faculty: "Công nghệ thông tin", Major: "Kỹ thuật phần mềm", Year: 2023, Avatar: "https://api.dicebear.com/7.x/bottts/svg?seed=huudung"),
            (Username: "sv_minhquan", Email: "minhquan@gmail.com", FullName: "Trần Minh Quân", Code: "SV000101", Faculty: "Công nghệ thông tin", Major: "Kỹ thuật phần mềm", Year: 2023, Avatar: "https://api.dicebear.com/7.x/bottts/svg?seed=minhquan"),
            (Username: "sv_ngocanh", Email: "ngocanh@gmail.com", FullName: "Lê Ngọc Ánh", Code: "SV000102", Faculty: "Công nghệ thông tin", Major: "Công nghệ thông tin", Year: 2023, Avatar: "https://api.dicebear.com/7.x/bottts/svg?seed=ngocanh"),
            (Username: "sv_ducthang", Email: "ducthang@gmail.com", FullName: "Phạm Đức Thắng", Code: "SV000103", Faculty: "Công nghệ thông tin", Major: "An toàn thông tin", Year: 2023, Avatar: "https://api.dicebear.com/7.x/bottts/svg?seed=ducthang")
        };

        var studentEntities = new List<Student>();
        foreach (var s in studentsData)
        {
            var user = await userManager.FindByNameAsync(s.Username);
            if (user is null)
            {
                user = new User
                {
                    UserName = s.Username,
                    Email = s.Email,
                    FullName = s.FullName,
                    PhoneNumber = "0912345678",
                    EmailConfirmed = true,
                    IsEmailVerified = true,
                    IsActive = true
                };
                var createRes = await userManager.CreateAsync(user, "Student@12345");
                if (createRes.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                    var student = new Student
                    {
                        UserId = user.Id,
                        User = user,
                        StudentCode = s.Code,
                        Faculty = s.Faculty,
                        Major = s.Major,
                        EnrollmentYear = s.Year,
                        AvatarUrl = s.Avatar
                    };
                    db.Students.Add(student);
                    studentEntities.Add(student);
                }
            }
            else
            {
                var stu = await db.Students.FindAsync(user.Id);
                if (stu != null) studentEntities.Add(stu);
            }
        }
        await db.SaveChangesAsync();

        // 6. Seed Lớp học phần (CourseClasses)
        var webSubject = await db.Subjects.FirstOrDefaultAsync(s => s.Code == "INT105");
        var mobileSubject = await db.Subjects.FirstOrDefaultAsync(s => s.Code == "INT106");
        var dbSubject = await db.Subjects.FirstOrDefaultAsync(s => s.Code == "INT104");

        var gvAn = lecturerEntities.FirstOrDefault(l => l.LecturerCode == "GV000001") ?? await db.Lecturers.FirstOrDefaultAsync();
        var gvBinh = lecturerEntities.FirstOrDefault(l => l.LecturerCode == "GV000002") ?? gvAn;
        var gvCuong = lecturerEntities.FirstOrDefault(l => l.LecturerCode == "GV000003") ?? gvAn;

        if (webSubject != null && gvAn != null)
        {
            var classWeb = await db.CourseClasses.FirstOrDefaultAsync(c => c.Code == "24D1INT10501");
            if (classWeb is null)
            {
                classWeb = new CourseClass
                {
                    Code = "24D1INT10501",
                    SubjectId = webSubject.Id,
                    LecturerId = gvAn.UserId,
                    Semester = "Học kỳ 1",
                    AcademicYear = 2024,
                    Capacity = 40
                };
                db.CourseClasses.Add(classWeb);
                await db.SaveChangesAsync();

                // Thêm sinh viên vào lớp
                foreach (var st in studentEntities)
                {
                    db.Enrollments.Add(new Enrollment
                    {
                        CourseClassId = classWeb.Id,
                        StudentId = st.UserId,
                        EnrolledAtUtc = DateTime.UtcNow,
                        Status = EnrollmentStatus.Active
                    });
                }

                // Thêm buổi học mẫu
                db.ClassSessions.Add(new ClassSession
                {
                    CourseClassId = classWeb.Id,
                    StartsAtUtc = DateTime.UtcNow.Date.AddHours(7).AddMinutes(30),
                    EndsAtUtc = DateTime.UtcNow.Date.AddHours(11).AddMinutes(30),
                    Room = "A1.01",
                    IsOpenForAttendance = false
                });

                db.ClassSessions.Add(new ClassSession
                {
                    CourseClassId = classWeb.Id,
                    StartsAtUtc = DateTime.UtcNow.Date.AddDays(7).AddHours(7).AddMinutes(30),
                    EndsAtUtc = DateTime.UtcNow.Date.AddDays(7).AddHours(11).AddMinutes(30),
                    Room = "A1.01",
                    IsOpenForAttendance = false
                });

                await db.SaveChangesAsync();
            }
        }

        if (mobileSubject != null && gvBinh != null)
        {
            var classMobile = await db.CourseClasses.FirstOrDefaultAsync(c => c.Code == "24D1INT10601");
            if (classMobile is null)
            {
                classMobile = new CourseClass
                {
                    Code = "24D1INT10601",
                    SubjectId = mobileSubject.Id,
                    LecturerId = gvBinh.UserId,
                    Semester = "Học kỳ 1",
                    AcademicYear = 2024,
                    Capacity = 40
                };
                db.CourseClasses.Add(classMobile);
                await db.SaveChangesAsync();

                // Thêm sinh viên vào lớp
                foreach (var st in studentEntities)
                {
                    db.Enrollments.Add(new Enrollment
                    {
                        CourseClassId = classMobile.Id,
                        StudentId = st.UserId,
                        EnrolledAtUtc = DateTime.UtcNow,
                        Status = EnrollmentStatus.Active
                    });
                }

                // Buổi học mẫu
                db.ClassSessions.Add(new ClassSession
                {
                    CourseClassId = classMobile.Id,
                    StartsAtUtc = DateTime.UtcNow.Date.AddHours(13),
                    EndsAtUtc = DateTime.UtcNow.Date.AddHours(17),
                    Room = "B2.03",
                    IsOpenForAttendance = false
                });

                await db.SaveChangesAsync();
            }
        }

        if (dbSubject != null && gvCuong != null)
        {
            var classDb = await db.CourseClasses.FirstOrDefaultAsync(c => c.Code == "24D1INT10401");
            if (classDb is null)
            {
                classDb = new CourseClass
                {
                    Code = "24D1INT10401",
                    SubjectId = dbSubject.Id,
                    LecturerId = gvCuong.UserId,
                    Semester = "Học kỳ 1",
                    AcademicYear = 2024,
                    Capacity = 45
                };
                db.CourseClasses.Add(classDb);
                await db.SaveChangesAsync();

                // Buổi học mẫu
                db.ClassSessions.Add(new ClassSession
                {
                    CourseClassId = classDb.Id,
                    StartsAtUtc = DateTime.UtcNow.Date.AddHours(13),
                    EndsAtUtc = DateTime.UtcNow.Date.AddHours(17),
                    Room = "C3.05",
                    IsOpenForAttendance = false
                });

                await db.SaveChangesAsync();
            }
        }
    }
}
