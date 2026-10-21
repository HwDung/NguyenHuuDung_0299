using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenHuuDung_0299.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceQrAndFaceRecognition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendanceExpiresAtUtc",
                table: "ClassSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapturedFaceUrl",
                table: "AttendanceRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceInfo",
                table: "AttendanceRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFaceVerified",
                table: "AttendanceRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AttendanceRecords",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AttendanceRecords",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AttendanceExpiresAtUtc",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "CapturedFaceUrl",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "DeviceInfo",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "IsFaceVerified",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AttendanceRecords");
        }
    }
}
