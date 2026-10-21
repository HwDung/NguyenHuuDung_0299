using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NguyenHuuDung_0299.Models.Academic;
using NguyenHuuDung_0299.Models.Auth;
using NguyenHuuDung_0299.Models.Users;

namespace NguyenHuuDung_0299.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Major> Majors => Set<Major>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Lecturer> Lecturers => Set<Lecturer>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<CourseClass> CourseClasses => Set<CourseClass>();
    public DbSet<ClassSession> ClassSessions => Set<ClassSession>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<EmailOtp> EmailOtps => Set<EmailOtp>();
    public DbSet<LoginOtp> LoginOtps => Set<LoginOtp>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Faculty>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<Major>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<Student>().HasKey(x => x.UserId);
        modelBuilder.Entity<Student>().HasIndex(x => x.StudentCode).IsUnique();
        modelBuilder.Entity<Lecturer>().HasKey(x => x.UserId);
        modelBuilder.Entity<Lecturer>().HasIndex(x => x.LecturerCode).IsUnique();
        modelBuilder.Entity<Subject>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<CourseClass>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<Enrollment>().HasIndex(x => new { x.CourseClassId, x.StudentId }).IsUnique();
        modelBuilder.Entity<AttendanceRecord>().HasIndex(x => new { x.ClassSessionId, x.StudentId }).IsUnique();
        modelBuilder.Entity<EmailOtp>().HasIndex(x => new { x.Email, x.ExpiresAtUtc });
        modelBuilder.Entity<LoginOtp>().HasIndex(x => new { x.UserId, x.ExpiresAtUtc });

        modelBuilder.Entity<Major>()
            .HasOne(x => x.Faculty).WithMany(x => x.Majors)
            .HasForeignKey(x => x.FacultyId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(x => x.Student).WithOne(x => x.User)
            .HasForeignKey<Student>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>()
            .HasOne(x => x.Lecturer).WithOne(x => x.User)
            .HasForeignKey<Lecturer>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CourseClass>().HasOne(x => x.Subject).WithMany(x => x.CourseClasses).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CourseClass>().HasOne(x => x.Lecturer).WithMany(x => x.CourseClasses)
            .HasForeignKey(x => x.LecturerId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ClassSession>().HasOne(x => x.CourseClass).WithMany(x => x.Sessions).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Enrollment>().HasOne(x => x.CourseClass).WithMany(x => x.Enrollments).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Enrollment>().HasOne(x => x.Student).WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<AttendanceRecord>().HasOne(x => x.ClassSession).WithMany(x => x.AttendanceRecords).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<AttendanceRecord>().HasOne(x => x.Student).WithMany(x => x.AttendanceRecords)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<LoginOtp>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Lecturer", NormalizedName = "LECTURER" },
            new Role { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Student", NormalizedName = "STUDENT" });

        modelBuilder.Entity<Faculty>().HasData(
            new Faculty { Id = 1, Code = "CNTT", Name = "Công nghệ thông tin", Description = "Khoa Công nghệ thông tin" },
            new Faculty { Id = 2, Code = "QTKD", Name = "Quản trị kinh doanh", Description = "Khoa Quản trị kinh doanh" },
            new Faculty { Id = 3, Code = "TCNH", Name = "Tài chính - Ngân hàng", Description = "Khoa Tài chính - Ngân hàng" },
            new Faculty { Id = 4, Code = "DTVT", Name = "Điện tử - Viễn thông", Description = "Khoa Điện tử - Viễn thông" },
            new Faculty { Id = 5, Code = "NN", Name = "Ngoại ngữ", Description = "Khoa Ngoại ngữ" }
        );

        modelBuilder.Entity<Major>().HasData(
            new Major { Id = 1, Code = "KTPM", Name = "Kỹ thuật phần mềm", FacultyId = 1, Description = "Chuyên ngành Kỹ thuật phần mềm" },
            new Major { Id = 2, Code = "CNTT", Name = "Công nghệ thông tin", FacultyId = 1, Description = "Chuyên ngành Công nghệ thông tin" },
            new Major { Id = 3, Code = "KHMT", Name = "Khoa học máy tính", FacultyId = 1, Description = "Chuyên ngành Khoa học máy tính" },
            new Major { Id = 4, Code = "ATTT", Name = "An toàn thông tin", FacultyId = 1, Description = "Chuyên ngành An toàn thông tin" },
            new Major { Id = 5, Code = "HTTT", Name = "Hệ thống thông tin", FacultyId = 1, Description = "Chuyên ngành Hệ thống thông tin" },
            new Major { Id = 6, Code = "QTKD", Name = "Quản trị kinh doanh", FacultyId = 2, Description = "Chuyên ngành Quản trị kinh doanh" },
            new Major { Id = 7, Code = "MKT", Name = "Marketing", FacultyId = 2, Description = "Chuyên ngành Marketing" },
            new Major { Id = 8, Code = "KDQT", Name = "Kinh doanh quốc tế", FacultyId = 2, Description = "Chuyên ngành Kinh doanh quốc tế" },
            new Major { Id = 9, Code = "TCNH", Name = "Tài chính - Ngân hàng", FacultyId = 3, Description = "Chuyên ngành Tài chính - Ngân hàng" },
            new Major { Id = 10, Code = "KT", Name = "Kế toán", FacultyId = 3, Description = "Chuyên ngành Kế toán" },
            new Major { Id = 11, Code = "KTDT", Name = "Kỹ thuật Điện tử", FacultyId = 4, Description = "Chuyên ngành Kỹ thuật Điện - Điện tử" },
            new Major { Id = 12, Code = "TDH", Name = "Tự động hóa", FacultyId = 4, Description = "Kỹ thuật Điều khiển và Tự động hóa" },
            new Major { Id = 13, Code = "NNA", Name = "Ngôn ngữ Anh", FacultyId = 5, Description = "Chuyên ngành Ngôn ngữ Anh" },
            new Major { Id = 14, Code = "NNN", Name = "Ngôn ngữ Nhật", FacultyId = 5, Description = "Chuyên ngành Ngôn ngữ Nhật" }
        );
    }
}
