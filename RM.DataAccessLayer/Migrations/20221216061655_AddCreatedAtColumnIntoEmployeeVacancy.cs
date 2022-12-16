using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RM.DataAccessLayer.Migrations
{
    public partial class AddCreatedAtColumnIntoEmployeeVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EmployeeVacancy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmployeeVacancy");
        }
    }
}
