using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NguyenHuuDung_0299.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyAndMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Majors_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "CNTT", "Khoa Công nghệ thông tin", "Công nghệ thông tin" },
                    { 2, "QTKD", "Khoa Quản trị kinh doanh", "Quản trị kinh doanh" },
                    { 3, "TCNH", "Khoa Tài chính - Ngân hàng", "Tài chính - Ngân hàng" },
                    { 4, "DTVT", "Khoa Điện tử - Viễn thông", "Điện tử - Viễn thông" },
                    { 5, "NN", "Khoa Ngoại ngữ", "Ngoại ngữ" }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "Id", "Code", "Description", "FacultyId", "Name" },
                values: new object[,]
                {
                    { 1, "KTPM", "Chuyên ngành Kỹ thuật phần mềm", 1, "Kỹ thuật phần mềm" },
                    { 2, "CNTT", "Chuyên ngành Công nghệ thông tin", 1, "Công nghệ thông tin" },
                    { 3, "KHMT", "Chuyên ngành Khoa học máy tính", 1, "Khoa học máy tính" },
                    { 4, "ATTT", "Chuyên ngành An toàn thông tin", 1, "An toàn thông tin" },
                    { 5, "HTTT", "Chuyên ngành Hệ thống thông tin", 1, "Hệ thống thông tin" },
                    { 6, "QTKD", "Chuyên ngành Quản trị kinh doanh", 2, "Quản trị kinh doanh" },
                    { 7, "MKT", "Chuyên ngành Marketing", 2, "Marketing" },
                    { 8, "KDQT", "Chuyên ngành Kinh doanh quốc tế", 2, "Kinh doanh quốc tế" },
                    { 9, "TCNH", "Chuyên ngành Tài chính - Ngân hàng", 3, "Tài chính - Ngân hàng" },
                    { 10, "KT", "Chuyên ngành Kế toán", 3, "Kế toán" },
                    { 11, "KTDT", "Chuyên ngành Kỹ thuật Điện - Điện tử", 4, "Kỹ thuật Điện tử" },
                    { 12, "TDH", "Kỹ thuật Điều khiển và Tự động hóa", 4, "Tự động hóa" },
                    { 13, "NNA", "Chuyên ngành Ngôn ngữ Anh", 5, "Ngôn ngữ Anh" },
                    { 14, "NNN", "Chuyên ngành Ngôn ngữ Nhật", 5, "Ngôn ngữ Nhật" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Code",
                table: "Faculties",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_Code",
                table: "Majors",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_FacultyId",
                table: "Majors",
                column: "FacultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
