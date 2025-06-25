using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyRim.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserChangedToIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_ApplicationUserId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_ApplicationUserId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_ApplicationUserId",
                table: "UserCourses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_ApplicationUserId",
                table: "UserCourses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
