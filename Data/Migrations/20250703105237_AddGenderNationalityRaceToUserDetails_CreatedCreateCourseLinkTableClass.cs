using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyRim.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderNationalityRaceToUserDetails_CreatedCreateCourseLinkTableClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "UserDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "UserDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CreatedCourses",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatedCourses", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CreatedCourses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreatedCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreatedCourses_CourseId",
                table: "CreatedCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreatedCourses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
